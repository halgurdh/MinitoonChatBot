// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MinitoonGames.Handlers;
using MinitoonGames.Mappings;
using MinitoonGames.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MinitoonGames.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        protected readonly IConfiguration Configuration;
        protected readonly ILogger Logger;

        private IIntentToDialogDictionary IntentToDialogDictionary;
        public MainDialog(IConfiguration configuration, IIntentToDialogDictionary intentToDialogDictionary, ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {
            Configuration = configuration;
            Logger = logger;

            IntentToDialogDictionary = intentToDialogDictionary;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new AboutDialog());
            AddDialog(new ShowGamesDialog(configuration));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                ActStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(Configuration["LuisAppId"]) || string.IsNullOrEmpty(Configuration["LuisAPIKey"]) || string.IsNullOrEmpty(Configuration["LuisAPIHostName"]))
            {
                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Text("NOTE: LUIS is not configured. To enable all capabilities, add 'LuisAppId', 'LuisAPIKey' and 'LuisAPIHostName' to the appsettings.json file."), cancellationToken);

                return await stepContext.NextAsync(null, cancellationToken);
            }
            else
            {
                return await stepContext.NextAsync(stepContext.Context.Activity, cancellationToken);
            }

        }
        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // If the child dialog ("BookingDialog") was cancelled or the user failed to confirm, the Result here will be null.
            UserInputHandler userInputHandler = null;

            if (stepContext.Context.Activity.Value != null)
            {
                userInputHandler = new UserInputHandler(stepContext.Context.Activity);
            }

            else if (stepContext.Context.Activity.Value == null)
            {
                var recogResult = await LuisHelper.ExecuteLuisQuery(Configuration, stepContext.Context, cancellationToken);
                userInputHandler = new UserInputHandler(recogResult);
            }

            if (IntentToDialogDictionary.ContainsKey(userInputHandler.Intent))
            {
                return await stepContext.BeginDialogAsync(IntentToDialogDictionary[userInputHandler.Intent], userInputHandler.Value, cancellationToken);
            }
            else
            {
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }

        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result != null)
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thank you."), cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thank you."), cancellationToken);
            }
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}

// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using MinitoonGames.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MinitoonGames.Dialogs
{
    public class ShowGamesDialog : ComponentDialog
    {
        protected readonly IConfiguration Configuration;

        public ShowGamesDialog(IConfiguration configuration)
             : base(nameof(ShowGamesDialog))
        {
            Configuration = configuration;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                ActStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Call LUIS and gather any potential booking details. (Note the TurnContext has the response to the prompt.)
            if (stepContext != null)
            {
                return await stepContext.NextAsync(stepContext, cancellationToken);
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
                // If the person search was successful tell the user. Otherwise tell person can not be found.
                var msg = "info";

                await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thank you."), cancellationToken);
            }
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}

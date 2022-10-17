// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MinitoonGames.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MinitoonGames
{
    public static class LuisHelper
    {
        public static async Task<RecognizerResult> ExecuteLuisQuery(IConfiguration configuration, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            try
            {
                // Create the LUIS settings from configuration.
                var luisApplication = new LuisApplication(
                    configuration["LuisAppId"],
                    configuration["LuisAPIKey"],
                    "https://" + configuration["LuisAPIHostName"]
                );

                var recognizer = new LuisRecognizer(luisApplication);

                // The actual call to LUIS
                return await recognizer.RecognizeAsync(turnContext, cancellationToken);

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

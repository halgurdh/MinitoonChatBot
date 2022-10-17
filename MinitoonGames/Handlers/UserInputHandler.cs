using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinitoonGames.Handlers
{
    public class UserInputHandler
    {
        internal string Text { get; set; }
        internal string Intent { get; set; }
        internal JObject Value { get; set; }
        public UserInputHandler()
        {
        }

        public UserInputHandler(Activity cardActivityResult)
        {
            Text = cardActivityResult.Text;
            Value = cardActivityResult.Value as JObject;
            Intent = Value["intent"].ToString();
        }

        public UserInputHandler(RecognizerResult userSentenceResult)
        {
            Text = userSentenceResult.Text;
            Value = userSentenceResult.Entities;
            Intent = userSentenceResult.GetTopScoringIntent().intent;
        }
    }
}

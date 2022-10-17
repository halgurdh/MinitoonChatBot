using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using MinitoonGames.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MinitoonGames.Dialogs
{
    public class AboutDialog : ComponentDialog
    {
        private readonly string DevicesDataSourcePath = Directory.GetCurrentDirectory() + @"\DataResources\Devices.csv";
        private readonly string InstructionsMapPath = Directory.GetCurrentDirectory() + @"\Resources";
        public AboutDialog() : base(nameof(AboutDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                AttachmentStepAsync
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }


        private async Task<DialogTurnResult> AttachmentStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            Activity replyToConversation = stepContext.Context.Activity.CreateReply("Here is the information about our game studio");
            replyToConversation.Attachments = new List<Attachment>();

            Dictionary<string, string> cardContentList = new Dictionary<string, string>();
            cardContentList.Add("Questionmark", "https://www.minitoongames.com/img/questionmark.png");

            foreach (KeyValuePair<string, string> cardContent in cardContentList)
            {
                var aboutDetails = new AboutDetails();

                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: cardContent.Value));

                List<CardAction> cardButtons = new List<CardAction>();

                CardAction plButton = new CardAction()
                {
                    Value = "https://www.minitoongames.com/#useit",
                    Type = "openUrl",
                    Title = "Visit minitoongames.com",
                    Image = "https://www.minitoongames.com/img/questionmark.png"
                };

                cardButtons.Add(plButton);

                HeroCard plCard = new HeroCard()
                {
                    Title = "About Minitoon Games:",
                    Subtitle = aboutDetails.aboutInfo,
                    Images = cardImages,
                    Buttons = cardButtons
                };

                Attachment plAttachment = plCard.ToAttachment();
                replyToConversation.Attachments.Add(plAttachment);
            }

            var reply = await stepContext.Context.SendActivityAsync(replyToConversation, cancellationToken);

            return await stepContext.EndDialogAsync(cancellationToken);
        }

    }
}
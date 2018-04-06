using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace fepbot_qnamaker.Dialogs
{
    [Serializable]
    public class QnaDialog : QnAMakerDialog
    {
        
        public QnaDialog(QnAMakerService qnaService) : base(qnaService)
        {
            
        }

        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            //var firstAnswer = result.Answers.First().Answer;

            //Activity answer = ((Activity)context.Activity).CreateReply();

            //var answerData = firstAnswer.Split(';');

            //if (answerData.Length == 1)
            //{
            //    await context.PostAsync(firstAnswer);
            //    return;
            //}

            //var title = answerData[0];

            //var description = answerData[1];

            //var url = answerData[2];

            //var imageURL = answerData[3];

            //HeroCard card = new HeroCard
            //{
            //    Title = title,
            //    Subtitle = description
            //};

            //card.Buttons = new List<CardAction>
            //{
            //    new CardAction(ActionTypes.OpenUrl, "Go go go!", value:url)
            //};

            //card.Images = new List<CardImage>
            //{
            //    new CardImage(url = imageURL)
            //};

            //answer.Attachments.Add(card.ToAttachment());

            //await context.PostAsync(answer);

            PromptDialog.Choice(context: context,
                resume: );
        }
        public virtual async Task ChoiceReceivedAsync(IDialogContext context, IAwaitable<AnnuvalConferencePass> activity)

        {

            AnnuvalConferencePass response = await activity;

            context.Call<object>(new AnnualPlanDialog(response.ToString()), ChildDialogComplete);


        }

    }

    

}
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System.Threading;


namespace chatbot_controller.Dialogs
{
    [Serializable]
    public class QnaDialog : QnAMakerDialog
    {

        

        public QnaDialog(QnAMakerService qnaService) : base(qnaService)
        {

        }

        public QnaDialog() : base(new QnAMakerService(
            new QnAMakerAttribute(
                ConfigurationManager.AppSettings["QnaSubscriptionKey"],
                ConfigurationManager.AppSettings["QnaKnowledgebaseId"], 
                "Desculpe-me, mas não achei resposta para sua pergunta", 
                0.5, 1)))
        {

        }


        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            var firstAnswer = result.Answers.First().Answer;

            await context.PostAsync(firstAnswer);

            await context.Forward(new TicketDialog(), this.ResumeAfterTicketDialog, message, CancellationToken.None);

        }

        private async Task ResumeAfterTicketDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var whatever = await result;
                await context.PostAsync(whatever.ToString());
            }
            catch (Exception e)
            {
            }
            
        }
    }

    



}
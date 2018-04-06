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
                0.5)))
        {

        }

        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            var firstAnswer = result.Answers.First().Answer;

            Debug.WriteLine("answerData:" + firstAnswer);
            
            await context.PostAsync(firstAnswer);

            await context.Forward(new ConfirmationDialog(), ChildDialogComplete, message, CancellationToken.None);

        }

        public virtual async Task ChildDialogComplete(IDialogContext context, IAwaitable<object> response)
        {
            await response;
            context.Done(this);
        }

    }

    



}
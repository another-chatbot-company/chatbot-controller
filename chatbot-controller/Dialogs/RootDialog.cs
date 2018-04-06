using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Diagnostics;

namespace chatbot_controller.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.Forward(new QnaDialog(), ResumeAfterQnaDialog, activity, CancellationToken.None);

        }

        private async Task ResumeAfterQnaDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            Debug.WriteLine("Entrou no [RootDialog] ResumeAfterQnaDialog");
            var activity = await result as Activity;
            context.Done(this);

        }
    }
}
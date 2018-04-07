using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Diagnostics;
using System.Collections.Generic;

namespace chatbot_controller.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        IEnumerable<string> options = new List<string> { "Sim", "Não" };

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Só um minuto...");
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result; // We've got a message!

            await context.Forward(new QnaDialog(), this.ResumeAfterQnaDialog, message, CancellationToken.None);
           
        }

        private async Task ResumeAfterQnaDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }

            // Again, wait for the next message from the user.
            //context.Wait(this.MessageReceivedAsync);

        }


    }
}
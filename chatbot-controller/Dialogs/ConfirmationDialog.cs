using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;

namespace chatbot_controller.Dialogs
{
    [Serializable]
    public class ConfirmationDialog : IDialog<object>
    {
        IEnumerable<string> options = new List<string> { "Sim", "Não" };

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var r = await result;

            PromptDialog.Choice(
                   context: context,
                   resume: ChoiceReceivedAsync,
                   options: this.options,
                   prompt: "Essa resposta atendeu plenamente a sua dúvida?",
                   retry: "Poderia refazer sua pergunta. Por favor?",
                   promptStyle: PromptStyle.Auto
               );
        }

        public virtual async Task ChoiceReceivedAsync(IDialogContext context, IAwaitable<String> activity)
        {

            String response = await activity;
            Debug.WriteLine("O usuário disse:" + response);

        }

        public virtual async Task ChildDialogComplete(IDialogContext context, IAwaitable<object> response)
        {

            await context.PostAsync("Obrigado pela sua resposta!.");
            context.Done(this);

        }

    }
}
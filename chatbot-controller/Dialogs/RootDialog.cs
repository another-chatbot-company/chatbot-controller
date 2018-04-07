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
            await context.PostAsync("");
            context.Wait(this.ShowTicketDialog);
            
        }

        public virtual async Task ShowTicketDialog(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var message = await activity;
            PromptDialog.Choice(
                context: context,
                resume: ChoiceReceivedAsync,
                options: this.options,
                prompt: "Essa resposta foi boa o suficiente?",
                retry: "Talvez seja melhor tentar outra...",
                promptStyle: PromptStyle.Auto
                );
        }
        public virtual async Task ChoiceReceivedAsync(IDialogContext context, IAwaitable<string> activity)
        {
            var response = await activity;

            context.Call<object>(new TicketDialog(), ChildDialogComplete);


        }

        public virtual async Task ChildDialogComplete(IDialogContext context, IAwaitable<object> response)
        {
            await context.PostAsync("Muito obrigado pela atenção!");
            context.Done(this);
        }



    }
}
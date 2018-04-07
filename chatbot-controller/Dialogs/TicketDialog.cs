using chatbot_controller.Models;
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
    public class TicketDialog : IDialog<object>
    {
        //Ticket ticket;
        string name;
        string email;
        string phone;

        private IEnumerable<string> options = new List<string> { "Sim", "Não"};

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Sua dúvida foi esclarecida?");
            context.Wait(this.OpenDialog);

        }

        public async Task OpenDialog(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {

            var response = await activity;

            PromptDialog.Choice(
                context: context,
                resume: MessageReceivedAsync,
                options: this.options,
                prompt: "Escolha uma das opções abaixo",
                retry: "Tente novamente. Por favor.",
                promptStyle: PromptStyle.Auto
                );

        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<string> answer)
        {
            try
            {
                var response1 = await answer;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

            var response = "sim";
            if (response.ToLower().Equals("sim"))
            {
                PromptDialog.Text(
                    context: context,
                    resume: ResumeGetName,
                    prompt: "Please share your good name",
                    retry: "Sorry, I didn't understand that. Please try again."
                );
            }
            else {
                await context.PostAsync("Que pena...\nPosso lhe ajudar a abrir um ticket então?");
                context.Done(this);
            }

        }

        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> Username)
        {
            string response = await Username;
            name = response;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetEmail,
                prompt: "Please share your Email ID",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }

        public virtual async Task ResumeGetEmail(IDialogContext context, IAwaitable<string> UserEmail)
        {
            string response = await UserEmail;
            email = response; ;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetPhone,
                prompt: "Please share your Mobile Number",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }
        public virtual async Task ResumeGetPhone(IDialogContext context, IAwaitable<string> mobile)
        {
            string response = await mobile;
            phone = response;

            await context.PostAsync(String.Format("Hello {0} ,Congratulation :) Your  C# Corner Annual Conference 2018 Registrion Successfullly completed with Name = {0} Email = {1} Mobile Number {2} . You will get Confirmation email and SMS", name, email, phone));

            context.Done(this);
        }
        
    }
}
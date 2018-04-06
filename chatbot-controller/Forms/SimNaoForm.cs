using chatbot_controller.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatbot_controller.Forms
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe-me, não entendi: \"{0}\". Poderia reescrever de outra forma, por favor?")]
    public class SimNaoForm
    {

        public static IForm<Ticket> BuildForm()
        {
            var form = new FormBuilder<Ticket>();
            
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Message("Olá, Seja bem-vindo(a)!");

            form.OnCompletion(async (context, ticket) =>
            {
                await context.PostAsync("Seu pedido foi gerado");
            });

            return form.Build();
        }

    }

    public enum Opcoes
    {
        //Não são case sensitive
        [Terms("Sim", "S")]
        Sim = 1,
        [Terms("Não", "n")]
        Nao
    }
}
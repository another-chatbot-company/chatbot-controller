﻿using fepbot_qnamaker.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepbot_qnamaker.Forms
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe-me, não entendi\"{0}\".")]
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
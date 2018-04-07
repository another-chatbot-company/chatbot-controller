using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Freshdesk;
using System.Configuration;
using System;
using System.Linq;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Collections.Generic;
using chatbot_controller.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System.Diagnostics;

namespace chatbot_controller
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 
        private static string ChatState = "qna";
        private static bool hasQnAAnswered = false;

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            Debug.WriteLine("ChatState Global:" + ChatState);
            if (activity.Type == ActivityTypes.Message)
            {
                var userMessage = activity.Text.ToString().ToLower();

                if (ChatState.Equals("qna"))
                {
                    if (!hasQnAAnswered)
                    {
                        Debug.WriteLine("===================== chamou QNADIALOG ==========================");
                        await Conversation.SendAsync(activity, () => new QnaDialog());
                        hasQnAAnswered = true;

                        if (!ChatState.Equals("ticket")) { 
                            var reply = activity.CreateReply("Essa resposta sanou sua dúvida (Sim ou Não)?");
                            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                    
                    }
                    else
                    {
                        if (userMessage.Contains("sim") || userMessage.Equals("s"))
                        {
                            var reply = activity.CreateReply("Que bom! Meu treinamento está compensando!\nAté a próxima!");
                            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                            await connector.Conversations.ReplyToActivityAsync(reply);

                            hasQnAAnswered = false;
                            ChatState = "qna";
                        }
                        else if (userMessage.Contains("nao") || userMessage.Equals("n") || userMessage.Equals("não"))
                        {
                            var reply = activity.CreateReply("Que pena...\nPosso lhe ajudar a abrir um ticket então?");
                            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                            await connector.Conversations.ReplyToActivityAsync(reply);

                            Debug.WriteLine("ChatState1:" + ChatState);
                            hasQnAAnswered = true;
                            ChatState = "ticket";
                            Debug.WriteLine("ChatState2:" + ChatState);
                        }
                        else
                        {
                            var reply = activity.CreateReply("Perdão, não entendi");
                            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
                else if (ChatState.Equals("ticket"))
                {
                    Debug.WriteLine("Está no local correto!");
                    if (userMessage.Contains("sim") || userMessage.Equals("s"))
                    {
                        
                        await Conversation.SendAsync(activity, () => new TicketDialog());
                        hasQnAAnswered = false;
                        ChatState = "ticket";
                    }
                    else if (userMessage.Contains("nao") || userMessage.Equals("n") || userMessage.Equals("não"))
                    {
                        var reply = activity.CreateReply("Tudo bem, então! Obrigado pela paciência e até mais!");
                        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                        await connector.Conversations.ReplyToActivityAsync(reply);

                        hasQnAAnswered = false;
                        ChatState = "qna";
                    }
                    else
                    {
                        var reply = activity.CreateReply("Perdão, não entendi");
                        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                }
        
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (activity.MembersAdded != null && activity.MembersAdded.Any())
                {
                    foreach (var member in activity.MembersAdded)
                    {
                        if (member.Id != activity.Recipient.Id)
                        {
                            var reply = activity.CreateReply("Olá! Eu sou o FEPBot. :)\n\nSou um robô em treinamento, mas posso ser mais ágil do que um ticket!\nDigite sua pergunta, por favor!");
                            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }
            

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

    }
}
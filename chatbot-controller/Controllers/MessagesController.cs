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

namespace fepbot_qnamaker
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                var freshdeskService = new FreshdeskService(
                    ConfigurationManager.AppSettings["FreshdeskAPIKey"], 
                    new Uri(ConfigurationManager.AppSettings["FreshdeskURL"]));

                var ticketResponse = freshdeskService.CreateTicket(new CreateTicketRequest()
                {
                    TicketInfo = new CreateTicketInfo()
                    {
                        Email = "wilecoyote@acme.com",
                        Subject = "ACME Super Outfit won't fly!!!",
                        Description = "I recently purchased an ACME Super Outfit because it was supposed to fly, but it's doesn't work!",
                        Priority = 1,
                        Status = 2
                    }
                });

                await Conversation.SendAsync(activity, () => new Dialogs.QnaDialog());
                
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (activity.MembersAdded != null && activity.MembersAdded.Any())
                {
                    foreach (var member in activity.MembersAdded)
                    {
                        if (member.Id != activity.Recipient.Id)
                        {
                            await this.SendConversation(activity);
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

        private async Task SendConversation(Activity activity)
        {
            await Conversation.SendAsync(activity, () => Chain.From(() => FormDialog.FromForm(() => Form.Pedido.BuildForm(), FormOptions.PromptFieldsWithValues)));
        }
    }

    
}
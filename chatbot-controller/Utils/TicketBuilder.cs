using Freshdesk;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace fepbot_qnamaker.Utils
{
    public class TicketBuilder
    {

        public void buildTicket()
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
        }
    }
}
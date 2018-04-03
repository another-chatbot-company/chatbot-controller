using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepbot_qnamaker.Models
{
    public class Ticket
    {
        [JsonProperty("cc_emails")]
        public object[] CC_Emails { get; set; }

        [JsonProperty("fwd_emails")]
        public object[] FWD_Emails { get; set; }

        [JsonProperty("reply_cc_emails")]
        public object[] Reply_CC_Emails { get; set; }

        [JsonProperty("fr_escalated")]
        public bool FR_Escalated { get; set; }

        [JsonProperty("spam")]
        public bool Spam { get; set; }

        [JsonProperty("email_config_id")]
        public long Email_Config_ID { get; set; }

        [JsonProperty("group_id")]
        public object Group_ID { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("requester_id")]
        public long Requester_ID { get; set; }

        [JsonProperty("responder_id")]
        public object Responder_ID { get; set; }

        [JsonProperty("source")]
        public int Source { get; set; }

        [JsonProperty("company_id")]
        public object Company_ID { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("to_emails")]
        public string[] To_Emails { get; set; }

        [JsonProperty("product_id")]
        public object Product_ID { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("type")]
        public object Type { get; set; }

        [JsonProperty("due_by")]
        public DateTime Due_By { get; set; }

        [JsonProperty("fr_due_by")]
        public DateTime FR_Due_By { get; set; }

        [JsonProperty("is_escalated")]
        public bool Is_Escalated { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_text")]
        public string Description_Text { get; set; }

        [JsonProperty("custom_fields")]
        public CustomFields Custom_Fields { get; set; }

        [JsonProperty("created_at")]
        public DateTime Created_At { get; set; }

        [JsonProperty("updated_at")]
        public DateTime Updated_At { get; set; }

        public class CustomFields
        {
        }

    }
}
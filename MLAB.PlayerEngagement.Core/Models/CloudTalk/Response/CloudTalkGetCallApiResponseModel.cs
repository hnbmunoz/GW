using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CloudTalk.Response
{

    public class CloudTalkGetCallApiResponseModel
    {
        [JsonPropertyName("responseData")]
        public CloudTalkGetCallApiResponsDataModel ResponseData { get; set; }
    }
    public class CloudTalkGetCallApiResponsDataModel
    {
        [JsonPropertyName("itemsCount")]
        public int ItemsCount { get; set; }
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        [JsonPropertyName("data")]
        public List<CloudTalkGetCallDataModel> Data { get; set; }
    }

    public class CloudTalkGetCallDataModel
    {

        public CloudTalkCdr Cdr { get; set; }
        public CloudTalkCallNumber CallNumber { get; set; }
        public CloudTalkAgent Agent { get; set; }
        public List<CloudTalkNote> Notes { get; set; }
    }

    public class CloudTalkAgent
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Language { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Default_outbound_number { get; set; }
        public List<string> Associated_numbers { get; set; }
        public List<object> Groups { get; set; }
    }

    
    public class CloudTalkCallNumber
    {
        public string Id { get; set; }
        [JsonPropertyName("Internal_name")]
        public object InternalName { get; set; }
        [JsonPropertyName("Caller_id_e164")]
        public string CallerIdE164 { get; set; }
        [JsonPropertyName("Country_code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("Area_code")]
        public string AreaCode { get; set; }
    }

    public class CloudTalkNote
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
    }
    public class CloudTalkCdr
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("billsec")]
        public string Billsec { get; set; }
        public string Type { get; set; }
        [JsonPropertyName("public_external")]
        public string PublicExternal { get; set; }
        [JsonPropertyName("public_internal")]
        public string PublicInternal { get; set; }
        public bool Recorded { get; set; }
        [JsonPropertyName("is_voicemail")]
        public bool IsVoicemail { get; set; }
        [JsonPropertyName("fax_email")]
        public object Fax_email { get; set; }
        [JsonPropertyName("is_redirected")]
        public string IsRedirected { get; set; }
        [JsonPropertyName("redirected_from")]
        public string RedirectedFrom { get; set; }
        [JsonPropertyName("transferred_from")]
        public object TransferredFrom { get; set; }
        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        [JsonPropertyName("talking_time")]
        public string TalkingTime { get; set; }
        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }
        [JsonPropertyName("answered_at")]
        public DateTime AnsweredAt { get; set; }
        [JsonPropertyName("ended_at")]
        public DateTime EndedAt { get; set; }
        [JsonPropertyName("waiting_time")]
        public int WaitingTime { get; set; }
        [JsonPropertyName("wrapup_time")]
        public string WrapupTime { get; set; }
        [JsonPropertyName("recording_link")]
        public string RecordingLink { get; set; }
    }
}

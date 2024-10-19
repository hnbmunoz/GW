using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CloudTalk.Request
{
    public class UpdateCloudTalkCdrByCallingCodeRequestModel
    {
        public string CallingCode { get; set; }
        public int CallId { get; set; }
        public string Type { get; set; }
        public int Billsec { get; set; }
        public int CloudTalkUserId { get; set; }
        public string PublicInternal { get; set; }
        public int TalkingTime { get; set; }
        public int UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime AnsweredAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int WaitingTime { get; set; }
        public string RecordingLink { get; set; }
    }
}

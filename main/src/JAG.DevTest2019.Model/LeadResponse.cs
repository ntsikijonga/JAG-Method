using System;
using System.Net;
using System.Runtime.Serialization;

namespace JAG.DevTest2019.Model
{
    [Serializable]
    [DataContract]
    public class LeadResponse
    {
        public LeadResponse ()
        {
            IsSuccessful = false;
            LeadId = -1;
            Messages = new string [] { };
        }
        [DataMember]
        public long LeadId { get; set; }

        [DataMember]
        public bool IsSuccessful { get; set; }

        [DataMember]
        public bool IsDuplicate { get; set; }

        [DataMember]
        public bool IsCapped { get; set; }

        [DataMember]
        public string[] Messages { get; set; }

    }
}

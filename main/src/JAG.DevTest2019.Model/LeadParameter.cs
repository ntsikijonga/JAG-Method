using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace JAG.DevTest2019.Model
{
    [Serializable]
    [DataContract]
    public class LeadParameter
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}

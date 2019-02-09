using System;
using System.Collections.Generic;
using System.Linq;

namespace JAG.DevTest2019.Model
{
    public class Lead
    {
        #region Headers
        public string ClientIpAddress { get; set; }
        public string ReferrerUrl { get; set; }
        public string UserAgent { get; set; }
        #endregion Headers

        public string Gender { get; set; }

        public string Language { get; set; }

        public string BloodType { get; set; }

        #region Required system data
        public long LeadId { get; set; }
        public string TrackingCode { get; set; }
        #endregion Required system data

        #region Standard properties
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public virtual string ContactNumber { get; set; }
        #endregion Standard properties

        public string SessionId { get; set; }

        //All other properties are stored into a list of parameters
        #region Lead parameters
        public LeadParameter[] LeadParameters { get; set; }

        public IDictionary<string, string> GetLeadRequestParmeterDictionary ()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (LeadParameters != null)
            {
                result = LeadParameters.ToDictionary(item => item.Key, item => item.Value);
            }
            return result;
        }

        public void PopulateLeadRequestParmeterDictionary()
        {
            LeadParameters = null;
            var leadParameters = new List<LeadParameter>();
            if (!string.IsNullOrEmpty(BloodType))
            {
                leadParameters.Add(new LeadParameter() { Key = "BloodType", Value = BloodType });
            }
            if (!string.IsNullOrEmpty(BloodType))
            {
                leadParameters.Add(new LeadParameter() { Key = "Gender", Value = Gender });
            }
            if (!string.IsNullOrEmpty(BloodType))
            {
                leadParameters.Add(new LeadParameter() { Key = "Language", Value = Language });
            }
            LeadParameters = leadParameters.ToArray();
        }
        #endregion Lead parameters
    }
}

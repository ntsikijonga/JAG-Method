using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JAG.DevTest2019.Host.Models
{
    public class LeadResultViewModel
    {
        public long LeadId { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
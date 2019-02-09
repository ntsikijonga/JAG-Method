using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JAG.DevTest2019.Host.Models
{
    public class LeadViewModel
    {

        public LeadViewModel()
        {
            Results = new LeadResultViewModel();
        }

        public string TrackingCode { get; }

        [Display(Name = "Select your gender?")]
        public string Gender { get; set; }

        [Display(Name = "Select your language?")]
        public string Language { get; set; }

        [Display(Name = "Select your blood type?")]
        public string BloodType { get; set; }

        [Required(ErrorMessage = "You must provide your First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must provide your Surname")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        public string SessionId { get; set; }

        public decimal ServiceQueriesNumber { get; set; } = 6000000;

        //public string ServicedQueriesNumberString { get { return ServiceQueriesNumber}; }
    
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email Address (Valid Email)")]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Display(Name = "Contact Number (000-000-0000)")]
        public virtual string ContactNumber { get; set; }

        public List<SelectListItem> Genders { get; } = new List<SelectListItem>()
        {
            new SelectListItem()  {Text="Male", Value = "Male"},
            new SelectListItem()  {Text="Female", Value = "Female"},
        };

        public List<SelectListItem> Languages { get; } = new List<SelectListItem>()
        {
            new SelectListItem()  {Text="English", Value = "English"},
            new SelectListItem()  {Text="Afrikaans", Value = "Afrikaans"},
            new SelectListItem()  {Text="Zulu", Value = "Zulu"},
            new SelectListItem()  {Text="Xhosa", Value = "Xhosa"},
            new SelectListItem()  {Text="Sesotho ", Value = "Sesotho"},
        };

        public List<SelectListItem> BloodTypes { get; } = new List<SelectListItem>()
        {
            new SelectListItem()  {Text="O+", Value = "O+"},
            new SelectListItem()  {Text="O-", Value = "O-"},
            new SelectListItem()  {Text="A+", Value = "A+"},
            new SelectListItem()  {Text="A-", Value = "A-"},
            new SelectListItem()  {Text="B+", Value = "B+"},
            new SelectListItem()  {Text="B-", Value = "B-"},
            new SelectListItem()  {Text="AB+", Value = "AB+"},
            new SelectListItem()  {Text="AB-", Value = "AB-"},
        };

        public LeadResultViewModel Results { get; set; }
    }
}
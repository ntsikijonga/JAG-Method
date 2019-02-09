using JAG.DevTest2019.Host.Models;
using JAG.DevTest2019.Host.signalr.hubs;
using JAG.DevTest2019.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JAG.DevTest2019.Host.Controllers
{
  public class LeadController : Controller
  {
	public ActionResult Index()
	{
	  return View(new LeadViewModel());
	}

	[HttpPost]
	public async Task<ActionResult> SubmitLead(LeadViewModel model)
	{
	  try
	  {
		//TODO: 6. Call the WebAPI service here & pass results to UI

		string apiUrl = "http://localhost:8099/api/lead/submit";

		string sessionID = HttpContext.Session.SessionID;

		LeadResponse apiResponse = new LeadResponse();

		using (HttpClient client = new HttpClient())
		{
		  client.BaseAddress = new Uri(apiUrl);
		  client.DefaultRequestHeaders.Accept.Clear();
		  client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

		  HttpResponseMessage response = await client.PostAsync<LeadViewModel>(apiUrl, model, new JsonMediaTypeFormatter());
		  if (response.IsSuccessStatusCode)
		  {
			var data = await response.Content.ReadAsStringAsync();
			apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LeadResponse>(data);

		  }


		}

		LeadViewModel result = new LeadViewModel()
		{
		  Results = new LeadResultViewModel()
		  {
			LeadId = new Random().Next(),
			IsSuccessful = true,

			Message = String.Join("", apiResponse.Messages)
		  }
		};

		return View("Index", result);
	  }
	  catch(Exception ex)
	  {
		return View("Index", ex.Message);
	  }
	  
	}
  }
}
using JAG.DevTest2019.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace JAG.DevTest2019.LeadService.Controllers
{
  public class LeadController : ApiController
  {
	private string _connectionString = @"Server=localhost\SQLEXPRESS;Database=JAG2019;Trusted_Connection=True;";

	[HttpPost]
	[ResponseType(typeof(LeadResponse))]
	[Route("api/lead/submit")]
	public HttpResponseMessage Post(Lead request)
	{
	  LeadResponse response = new LeadResponse()
	  {
		LeadId = 10000000 + new Random().Next(),
		IsCapped = false,
		Messages = new[] { "Thank you for submitting your details,", "" },
		IsSuccessful = true,
		IsDuplicate = false
	  };

	  request.LeadId = response.LeadId;

	  if (!CheckIfExists(request))
		InsertData(request);
	  else
	  {
		response.IsDuplicate = true;
		response.IsSuccessful = false;
		response.Messages = new[] { "duplicate on [email/ contact number],", "" };
	  }

	  response.Messages[1] = $" your ID is { response.LeadId}";
	  UpdateLead(response, request);

	  Console.WriteLine($"Lead received {request.FirstName} {request.Surname}");

	  return Request.CreateResponse(HttpStatusCode.OK, response);
	}

	private void InsertData(Lead request)
	{
	  // define INSERT query with parameters

	  string insertLeadQuery = GetInsertLeadSQL();
	  string insertLeadParamQuery = GetInsertLeadParamSQL();
	  request.PopulateLeadRequestParmeterDictionary();

	  // create connection and command
	  using (SqlConnection cn = new SqlConnection(_connectionString))
	  using (SqlCommand cmd = new SqlCommand(insertLeadQuery, cn))
	  {

		// define parameters and their values
		cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 255).Value = request.FirstName;
		cmd.Parameters.Add("@Lastname", SqlDbType.VarChar, 255).Value = request.Surname;
		cmd.Parameters.Add("@LeadId", SqlDbType.BigInt).Value = request.LeadId;
		cmd.Parameters.Add("@TrackingCode", SqlDbType.VarChar, 20).Value = "N/A";
		cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = request.EmailAddress;
		cmd.Parameters.Add("@ReceivedDateTime", SqlDbType.DateTime).Value = DateTime.Now;
		cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 20).Value = request.ContactNumber;

		cn.Open();
		SqlTransaction sqlTran = cn.BeginTransaction();
		cmd.Transaction = sqlTran;

		int rowsAffected = 0;
		try
		{
		  rowsAffected += cmd.ExecuteNonQuery();
		  cmd.CommandText = insertLeadParamQuery;
		  cmd.Parameters.Clear();

		  foreach (var item in request.LeadParameters)
		  {
			cmd.Parameters.Add("@LeadParameterId", SqlDbType.VarChar, 150).Value = 10000000 + new Random().Next();
			cmd.Parameters.Add("@LeadId", SqlDbType.BigInt).Value = request.LeadId;
			cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = item.Key;
			cmd.Parameters.Add("@Value", SqlDbType.VarChar, 200).Value = item.Value;

			rowsAffected += cmd.ExecuteNonQuery();

			cmd.Parameters.Clear();
		  }
		  sqlTran.Commit();
		}
		catch (Exception ex)
		{
		  sqlTran.Rollback();
		}
		finally
		{
		  cn.Close();
		}

	  }
	}

	private bool CheckIfExists(Lead request)
	{
	  // define INSERT query with parameters
	  string query = "SELECT LeadId from dbo.Lead WHERE  Email = @Email OR ContactNumber = @ContactNumber";

	  // create connection and command
	  using (SqlConnection cn = new SqlConnection(_connectionString))
	  using (SqlCommand cmd = new SqlCommand(query, cn))
	  {
		// define parameters and their values
		cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = request.EmailAddress;
		cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 255).Value = request.ContactNumber;

		// open connection, execute INSERT, close connection
		cn.Open();
		SqlDataReader reader = cmd.ExecuteReader();

		if (reader.HasRows)
		  return true;
		cn.Close();

		return false;
	  }
	}

	private string GetInsertLeadSQL()
	{
	  return "INSERT INTO dbo.Lead (LeadId, TrackingCode, FirstName, Lastname, ContactNumber, Email, ReceivedDateTime) " +
	  "VALUES(@LeadId, @TrackingCode, @FirstName, @Lastname, @ContactNumber, @Email, @ReceivedDateTime) ";
	}

	private string GetInsertLeadParamSQL()
	{
	  return "INSERT INTO dbo.LeadParameter (LeadParameterId, LeadId, Name, Value) " +
	  " VALUES (@LeadParameterId, @LeadId, @Name, @Value)";
	}

	private void UpdateLead(LeadResponse response, Lead request)
	{
	  string query = "UPDATE dbo.Lead SET IsDuplicate = @IsDuplicate, IsSuccessful = @IsSuccessful "+
		"WHERE  Email = @Email AND ContactNumber = @ContactNumber";

	  // create connection and command
	  using (SqlConnection cn = new SqlConnection(_connectionString))
	  using (SqlCommand cmd = new SqlCommand(query, cn))
	  {
		// define parameters and their values
		cmd.Parameters.Add("@IsDuplicate", SqlDbType.Bit).Value = response.IsDuplicate;
		cmd.Parameters.Add("@IsSuccessful", SqlDbType.Bit).Value = response.IsSuccessful;
		cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = request.EmailAddress;
		cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 255).Value = request.ContactNumber;

		// open connection, execute INSERT, close connection
		cn.Open();
		SqlDataReader reader = cmd.ExecuteReader();
	  }
	}


  }
}

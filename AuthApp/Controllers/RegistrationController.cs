using AuthApp.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace AuthApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Books").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UsernName,Password,Email,IsActive) VALUES('" + registration.UserName + "','" + registration.Password + "','" + registration.Email + "','" + registration.IsActive + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data inserted";
            } 
            else
            {
                return "Error";
            }
        }

        [HttpPost]
        [Route("login")]
        public string login(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Books").ToString());
            SqlDataAdapter dataAdapter= new SqlDataAdapter("SELECT * FROM Registration WHERE Email = '"+registration.Email+"' AND Password = '"+registration.Password+"' AND IsActive = 1",con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                return "Data Found";
            }
            else
            {
                return "Error!";
            }
        }
    }
}

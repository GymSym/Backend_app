using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Backend_app.Models;
using System.Runtime.Remoting.Messaging;

namespace Backend_app.Controllers
{
    public class TestController : ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;

        [HttpPost]
        [Route("Registration")]
        public string Registration(Employee employee)
        {
            string message = string.Empty;

            try
            {
                cmd = new SqlCommand("user_registration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);


                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    message = "Data inserted.";
                }
                else
                {
                    message = "Error at inserting data";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            

            return message;
        }

        [HttpPost]
        [Route("Login")]
        public string Login(Employee employee)
        {
            string message = string.Empty;

            try
            {
                da = new SqlDataAdapter("user_login",conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Name", employee.Name);
                da.SelectCommand.Parameters.AddWithValue("@PhoneNo",employee.PhoneNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    message = "User is valid";
                }
                else
                {
                    message = "User is Invalid";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }



            return message;
        }

    }
}

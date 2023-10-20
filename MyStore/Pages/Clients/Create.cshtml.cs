using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phoneNr"];
            clientInfo.address = Request.Form["address"];


			if (string.IsNullOrEmpty(clientInfo.name) || string.IsNullOrEmpty(clientInfo.email) || string.IsNullOrEmpty(clientInfo.phone) || string.IsNullOrEmpty(clientInfo.address))
			{
				errorMessage = "All the fields are required";
				return;
			}
			//save the data to the database

			try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Insert INTO clients " + "(name,email,phone,address) VALUES" +
                        "(@name,@email,@phone,@address);";
                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue ("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }

                }
			}
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = "";clientInfo.email = "";clientInfo.phone = "";clientInfo.address = "" +
                "";successMessage = "new client is added correclty";

            Response.Redirect("/Clients/Index");

        }
    }
}

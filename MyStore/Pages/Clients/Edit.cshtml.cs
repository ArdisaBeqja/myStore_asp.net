using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo =new ClientInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {
            String id = Request.Query["id"];
            //connect to the database
            try
            {
                String connectionString="Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM clients  WHERE id=@id";
                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {   //ka kto argments sepse duhet te dij ne cfare ne cilen db do jet query
                       
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name=reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);

                            }
                        }

                    }

                }
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phoneNr"];
            clientInfo.address = Request.Form["address"];

            if( clientInfo.id.Length==0||
                clientInfo.name.Length==0||
                clientInfo.email.Length==0||
                clientInfo.phone.Length==0||
                clientInfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using(SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
					String sql = "Update clients SET name=@name, email=@email, phone=@phoneNr, address=@address WHERE id=@id";

                    using (SqlCommand command= new SqlCommand(sql, connection)) {

                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phoneNr", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }

			}
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

            //Redirect to the user, Main page
			Response.Redirect("/Clients/Index");


		}
	}
}

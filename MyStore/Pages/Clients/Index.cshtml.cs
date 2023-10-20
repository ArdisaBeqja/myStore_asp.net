using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo>listClients=new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection= new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM clients";
                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {while(reader.Read())
                            {
								ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0) ;
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address=reader.GetString(4);
                                clientInfo.createdAt=reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);

							}
						}
                    }
                }
				foreach (var client in listClients)
				{
					Console.WriteLine($"ID: {client.id}, Name: {client.name}, Email: {client.email}, Phone: {client.phone}, Address: {client.address}, Created At: {client.createdAt}");
				}
			}
			catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ex.ToString());
            }
        }
    }
    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String createdAt;
    }
}

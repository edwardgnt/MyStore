using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=WEBDEV1;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo
                                {
                                    id = "" + reader.GetInt32(0),
                                    name = reader.GetString(1),
                                    email = reader.GetString(2),
                                    phone = reader.GetString(3),
                                    address = reader.GetString(4),
                                    created_at = reader.GetDateTime(5)
                                };

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "  + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String id { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String address { get; set; }
        public DateTime created_at { get; set; }
    }
}

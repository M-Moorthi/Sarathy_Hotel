using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages.Customer
{
    public class CustomerModel : PageModel
    {
        public List<CustomerInfo> list = new List<CustomerInfo>();
        public void OnGet()
        {
            try
            {
                string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sql = "select * from customer";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo row = new CustomerInfo();
                                row.Id = "" + reader.GetInt32(0);
                                row.Name = reader.GetString(1);
                                row.Gender = reader.GetString(2);
                                row.Phone = reader.GetString(3);
                                row.Email = reader.GetString(4);
                                row.Address = reader.GetString(5);
                                row.Date = reader.GetDateTime(6);
                                list.Add(row);
                            }
                        }
                    }
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class CustomerInfo
    {
        public string Id;
        public string Name;
        public string Gender;
        public string Phone;
        public string Email;
        public string Address;
        public DateTime Date;
    }
}

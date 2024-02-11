using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Hotel_Bluebird.Pages.Admin
{
    public class BooknowModel : PageModel
    {
        public List<BookingInfo> list = new List<BookingInfo>();
        public void OnGet()
        {
            try
            {
                string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sql = "select * from booknow";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<BookingInfo> bookingInfo = new List<BookingInfo>();
                            while (reader.Read())
                            {
                                BookingInfo row = new BookingInfo();
                                row.Id = "" + reader.GetInt32(0);
                                row.CheckIn = reader.GetString(1);
                                row.CheckOut = reader.GetString(2);
                                row.Adult = reader.GetString(3);
                                row.Child = reader.GetString(4);
                                row.Date = reader.GetDateTime(5);
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
    public class BookingInfo
    {
        public string Id;
        public string CheckIn;
        public string CheckOut;
        public string Adult;
        public string Child;
        public DateTime Date;
    }
}

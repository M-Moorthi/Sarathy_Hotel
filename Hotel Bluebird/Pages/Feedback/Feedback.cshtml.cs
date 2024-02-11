using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages.Feedback
{
    public class FeedbackModel : PageModel
    {
        public List<FeedInfo> list = new List<FeedInfo>();
        public void OnGet()
        {
            try
            {
                string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sql = "select * from feedback";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<FeedInfo> feedInfo = new List<FeedInfo>();
                            while (reader.Read())
                            {
                                FeedInfo row = new FeedInfo();
                                row.Id = "" + reader.GetInt32(0);
                                row.Name = reader.GetString(1);
                                row.Email = reader.GetString(2);
                                row.Message = reader.GetString(3);
                                row.Date = reader.GetDateTime(4);
                                list.Add(row);

                            }
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class FeedInfo
    {
        public string Id;
        public string Name;
        public string Email;
        public string Message;
        public DateTime Date;
    }
}

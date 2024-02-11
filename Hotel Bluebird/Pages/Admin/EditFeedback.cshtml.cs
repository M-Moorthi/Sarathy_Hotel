using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages.Admin
{
    public class EditFeedbackModel : PageModel
    {
        public FeedInfo feedInfo = new FeedInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string Id = Request.Query["id"];
            try
            {
                 string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                 using (SqlConnection con = new SqlConnection(conString))
                 {
                    con.Open();
                    string sql = "select * from feedback where Id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                feedInfo.Id = "" + reader.GetInt32(0);
                                feedInfo.Name = reader.GetString(1);
                                feedInfo.Email = reader.GetString(2);
                                feedInfo.Message = reader.GetString(3);

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

        public void OnPost()
        {
            try
            {
                string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sql = "update feedback set Name=@name, Email=@email, Message=@message where Id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@name", feedInfo.Name);
                        cmd.Parameters.AddWithValue("@email", feedInfo.Email);
                        cmd.Parameters.AddWithValue("@message", feedInfo.Message);
                        cmd.Parameters.AddWithValue("@id", feedInfo.Id);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            Response.Redirect("Admin/Feedback");
        }
    }
}

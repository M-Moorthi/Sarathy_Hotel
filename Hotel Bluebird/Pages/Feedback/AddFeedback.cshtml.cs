using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages.Feedback
{
    public class AddFeedbackModel : PageModel
    {
        public FeedInfo feedInfo = new FeedInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            // Your GET logic here (if needed)
        }

        public void OnPost()
        {
            // Retrieve form data from the request body
            feedInfo.Name = Request.Form["name"];
            feedInfo.Email = Request.Form["email"];
            feedInfo.Message = Request.Form["message"];

            if (string.IsNullOrWhiteSpace(feedInfo.Name) || string.IsNullOrWhiteSpace(feedInfo.Email) || string.IsNullOrWhiteSpace(feedInfo.Message))
            {
                errorMessage = "All the fields are required";
            }
            else
            {
                try
                {
                    string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        string sql = "INSERT INTO feedback (Name, Email, Message) VALUES(@name, @email, @message)";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@name", feedInfo.Name);
                            cmd.Parameters.AddWithValue("@email", feedInfo.Email);
                            cmd.Parameters.AddWithValue("@message", feedInfo.Message);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        successMessage = "Added successfully";
                    }

                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }

                Response.Redirect("/Admin/Feedback");
            }
        }
    }
}

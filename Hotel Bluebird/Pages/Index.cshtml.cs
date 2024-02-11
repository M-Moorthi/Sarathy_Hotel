using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public CheckavailInfo checkavailInfo = new CheckavailInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet() { }

        public void OnPost()
        {
            checkavailInfo.Check_In = Request.Form["check_in"];
            checkavailInfo.Check_Out = Request.Form["check_out"];
            checkavailInfo.Adult_Count = Request.Form["adult"];
            checkavailInfo.Child_Count = Request.Form["child"];


            try
            {
                string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sql = "INSERT INTO book_availability (Check_In, Check_Out, Adult_Count, Child_Count) VALUES (@check_in, @check_out, @adult, @child)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@check_in", checkavailInfo.Check_In);
                        cmd.Parameters.AddWithValue("@check_out", checkavailInfo.Check_Out);
                        cmd.Parameters.AddWithValue("@adult", checkavailInfo.Adult_Count);
                        cmd.Parameters.AddWithValue("@child", checkavailInfo.Child_Count);
                        cmd.ExecuteNonQuery();
                    }
                }
                successMessage = "Booking information saved successfully.";
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred: " + ex.Message;
            }

            Response.Redirect("/Admin/Booking");


        }
        
    }
    public class CheckavailInfo
    {
        public string Id;
        public string Check_In;
        public string Check_Out;
        public string Adult_Count;
        public string Child_Count;
        public string Date;
    }
}
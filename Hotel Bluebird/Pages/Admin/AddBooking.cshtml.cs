using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace Hotel_Bluebird.Pages.Admin
{
    public class AddBookingModel : PageModel
    {
        public BookingInfo bookingInfo = new BookingInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            bookingInfo.CheckIn = Request.Form["checkin"];
            bookingInfo.CheckOut = Request.Form["checkout"];
            bookingInfo.Adult = Request.Form["adult"];
            bookingInfo.Child = Request.Form["child"];

            if (bookingInfo.CheckIn == "" || bookingInfo.CheckOut == "" 
                || bookingInfo.Adult == "" || bookingInfo.Child == "")
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save the new client into database


            try
            {
                string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sql = "insert into booknow (CheckIn, CheckOut, Adult, Child) values (@checkin, @checkout, @adult, @child)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@checkin", bookingInfo.CheckIn);
                        cmd.Parameters.AddWithValue("@checkout", bookingInfo.CheckOut);
                        cmd.Parameters.AddWithValue("@adult", bookingInfo.Adult);
                        cmd.Parameters.AddWithValue("@child", bookingInfo.Child);
                        SqlDataAdapter sda = new SqlDataAdapter();
                        cmd.ExecuteNonQuery();

                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }




            Response.Redirect("/Admin/Booknow");


        }
    }
}

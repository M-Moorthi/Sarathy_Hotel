using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Hotel_Bluebird.Pages.Admin
{
    public class Edit_BookingModel : PageModel
    {
        public BookingInfo bookingInfo = new BookingInfo();
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
                    {
                        string sql = "select * from booknow where id=@id";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@id", Id);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    bookingInfo.Id = "" + reader.GetInt32(0);
                                    bookingInfo.CheckIn = reader.GetString(1);
                                    bookingInfo.CheckOut = reader.GetString(2);
                                    bookingInfo.Adult = reader.GetString(3);
                                    bookingInfo.Child = reader.GetString(4);
                                }
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
            bookingInfo.Id = Request.Form["id"];
            bookingInfo.CheckIn = Request.Form["checkin"];
            bookingInfo.CheckOut = Request.Form["checkout"];
            bookingInfo.Adult = Request.Form["adult"];
            bookingInfo.Child = Request.Form["child"];

            if (bookingInfo.Id == "" || bookingInfo.CheckIn == "" || bookingInfo.CheckOut == "" || bookingInfo.Adult == "" ||
                bookingInfo.Child == "")
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save the new client into database

            else 
            {
                try
                {
                    string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        string sql = "UPDATE booknow SET CheckIn=@checkin, CheckOut=@checkout, Adult=@adult, Child=@child  WHERE Id=@id";

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            cmd.Parameters.AddWithValue("@checkin", bookingInfo.CheckIn);
                            cmd.Parameters.AddWithValue("@checkout", bookingInfo.CheckOut);
                            cmd.Parameters.AddWithValue("@adult", bookingInfo.Adult);
                            cmd.Parameters.AddWithValue("@child", bookingInfo.Child);
                            cmd.Parameters.AddWithValue("@id", bookingInfo.Id);
                            cmd.ExecuteNonQuery();
                        }

                        successMessage = "Updated successfully";

                    }

                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }
            }
            

            // Redirect to the appropriate page after successful update
            Response.Redirect("/Admin/Booknow");
        }

    }
}

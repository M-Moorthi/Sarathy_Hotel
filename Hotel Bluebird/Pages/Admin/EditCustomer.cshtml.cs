using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages.Admin
{
    public class EditCustomerModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    {
                        string sql = "select * from customer where id=@id";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    customerInfo.Id = "" + reader.GetInt32(0);
                                    customerInfo.Name = reader.GetString(1);
                                    customerInfo.Gender = reader.GetString(2);
                                    customerInfo.Phone = reader.GetString(3);
                                    customerInfo.Email = reader.GetString(4);
                                    customerInfo.Address = reader.GetString(5);
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
            customerInfo.Id = Request.Form["id"];
            customerInfo.Name = Request.Form["name"];
            customerInfo.Gender = Request.Form["gender"];
            customerInfo.Phone = Request.Form["phone"];
            customerInfo.Email = Request.Form["email"];
            customerInfo.Address = Request.Form["address"];

            if (customerInfo.Id == "" || customerInfo.Name == "" || customerInfo.Gender == "" ||
                customerInfo.Phone == "" || customerInfo.Email == "" || customerInfo.Address == "")
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
                    string sql = "UPDATE customer SET Name=@name, Gender=@gender, Phone=@phone, Email=@email, Address=@address WHERE Id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@name", customerInfo.Name);
                        cmd.Parameters.AddWithValue("@gender", customerInfo.Gender);
                        cmd.Parameters.AddWithValue("@phone", customerInfo.Phone);
                        cmd.Parameters.AddWithValue("@email", customerInfo.Email);
                        cmd.Parameters.AddWithValue("@address", customerInfo.Address);
                        cmd.Parameters.AddWithValue("@id", customerInfo.Id);
                        cmd.ExecuteNonQuery();
                    }


                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Redirect to the appropriate page after successful update
            Response.Redirect("/Admin/customer");
        }
    }
}

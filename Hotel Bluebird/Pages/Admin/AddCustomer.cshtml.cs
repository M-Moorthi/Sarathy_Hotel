using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages.Admin
{
    public class AddCustomerModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            customerInfo.Name = Request.Form["name"];
            customerInfo.Gender = Request.Form["gender"];
            customerInfo.Phone = Request.Form["phone"];
            customerInfo.Email = Request.Form["email"];
            customerInfo.Address = Request.Form["address"];
           

            if (customerInfo.Name == "" || customerInfo.Gender == "" ||
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
                    string sql = "insert into customer (Name, Gender, Phone, Email, Address) values (@name, @gender, @phone, @Email, @address)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.AddWithValue("@name", customerInfo.Name);
                        cmd.Parameters.AddWithValue("@gender", customerInfo.Gender);
                        cmd.Parameters.AddWithValue("@phone", customerInfo.Phone);
                        cmd.Parameters.AddWithValue("@email", customerInfo.Email);
                        cmd.Parameters.AddWithValue("@address", customerInfo.Address);
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




            Response.Redirect("/Admin/Customer");


        }
    }
}

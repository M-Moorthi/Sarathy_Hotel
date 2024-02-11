using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Hotel_Bluebird.Pages.Register
{
    public class IndexModel : PageModel
    {
        public RegisterInfo registerInfo = new RegisterInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            registerInfo.Name = Request.Form["name"];
            registerInfo.Email = Request.Form["email"];
            registerInfo.Password = Request.Form["password"];
            registerInfo.CPassword = Request.Form["cpassword"];

            try
            {
                if (registerInfo.Password != registerInfo.CPassword)
                {
                    errorMessage = "Password Mismatch";
                }
                if (registerInfo.Password == registerInfo.CPassword)
                {
                    if (string.IsNullOrWhiteSpace(registerInfo.Name) || string.IsNullOrWhiteSpace(registerInfo.Email) || string.IsNullOrWhiteSpace(registerInfo.Password))
                    {
                        errorMessage = "All fields are required";

                    }
                    else
                    {
                        string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            con.Open();
                            string sql = "insert into tbl_reg (Name, Email, Password) values (@name, @email, @password)";
                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {
                                cmd.Parameters.AddWithValue("@name", registerInfo.Name);
                                cmd.Parameters.AddWithValue("@email", registerInfo.Email);
                                cmd.Parameters.AddWithValue("@password", registerInfo.Password);
                                cmd.ExecuteNonQuery();
                                successMessage = "Register successfully";
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
        public class RegisterInfo
        {
            public string Id;
            public string Name;
            public string Email;
            public string Password;
            public string CPassword;
            public string Date;
        }
    }
}

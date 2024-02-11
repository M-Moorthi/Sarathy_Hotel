using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;


namespace Hotel_Bluebird.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginInfo LoginInfo { get; set; }
        public string errorMessage = "";

        public string successMessage = "";

        public void OnGet()
        {
            // This is the GET handler for the login page.
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Ensure LoginInfo is not null
                if (LoginInfo != null)
                {
                    if (LoginInfo.Email == "admin@gmail.com" && LoginInfo.Password == "admin@123")
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                    else
                    {
                        try
                        {
                            string conString = "Data Source=LAPTOP-EDMJOBDI\\SQLEXPRESS;Initial Catalog=db_hotel;Integrated Security=True";
                            using (SqlConnection con = new SqlConnection(conString))
                            {
                                con.Open();
                                string sql = "SELECT Email, Password, Name FROM tbl_reg WHERE Email = @Email";
                                using (SqlCommand cmd = new SqlCommand(sql, con))
                                {
                                    cmd.Parameters.AddWithValue("@Email", LoginInfo.Email);

                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            string passwordFromDb = reader["Password"].ToString();
                                            if (LoginInfo.Password == passwordFromDb)
                                            {
                                                // Set the session variable
                                                HttpContext.Session.SetString("Name", reader["Name"].ToString());
                                                successMessage = "Login successfully";
                                                // Store the session data in TempData
                                                TempData["UserName"] = reader["Name"].ToString();

                                                return RedirectToPage("/Login/Home");
                                            }
                                        }
                                    }
                                }
                            }

                            errorMessage = "Invalid email or password.";
                        }
                        catch (Exception ex)
                        {
                            errorMessage = ex.Message;
                            Console.WriteLine(ex.ToString()); // Add this line for debugging
                        }
                    }
                }
            }

            // If authentication fails or LoginInfo is null, reload the login page with an error message.
            return Page();
        }


    }

    public class LoginInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

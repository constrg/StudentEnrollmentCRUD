using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentEnrollmentCRUD.Pages.Student
{
    public class CreateModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            studentInfo.fullname = Request.Form["fullname"];
            studentInfo.email = Request.Form["email"];
            studentInfo.mobile_no = Request.Form["mobile_no"];
            studentInfo.address = Request.Form["address"];
            studentInfo.gender = Request.Form["gender"];
            studentInfo.course = Request.Form["course"];
            studentInfo.subjects = Request.Form["subjects"];

            if (string.IsNullOrEmpty(studentInfo.fullname) ||
                string.IsNullOrEmpty(studentInfo.email) ||
                string.IsNullOrEmpty(studentInfo.mobile_no) ||
                string.IsNullOrEmpty(studentInfo.gender) ||
                string.IsNullOrEmpty(studentInfo.address) ||
                string.IsNullOrEmpty(studentInfo.course) ||
                string.IsNullOrEmpty(studentInfo.subjects))
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=WIN-N74MR2FKADF;Initial Catalog=StudentEnrollmentDb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Student" +
                                 "(fullname, email, mobile_no, address, gender, course, subjects) VALUES " +
                                 "(@fullname, @email, @mobile_no, @address, @gender, @course, @subjects);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fullname", studentInfo.fullname);
                        command.Parameters.AddWithValue("@email", studentInfo.email);
                        command.Parameters.AddWithValue("@mobile_no", studentInfo.mobile_no);
                        command.Parameters.AddWithValue("@address", studentInfo.address);
                        command.Parameters.AddWithValue("@gender", studentInfo.gender);
                        command.Parameters.AddWithValue("@course", studentInfo.course);
                        command.Parameters.AddWithValue("@subjects", studentInfo.subjects);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            studentInfo = new StudentInfo();
            successMessage = "New Student Added Successfully";
            Response.Redirect("/Student/Index");
        }
    }
}

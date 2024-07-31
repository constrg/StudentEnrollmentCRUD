using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentEnrollmentCRUD.Pages.Student
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=WIN-N74MR2FKADF;Initial Catalog=StudentEnrollmentDb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Student WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentInfo.id = reader.GetInt32(0).ToString();
                                studentInfo.fullname = reader.GetString(1);
                                studentInfo.email = reader.GetString(2);
                                studentInfo.mobile_no = reader.GetString(3);
                                studentInfo.address = reader.GetString(4);
                                studentInfo.gender = reader.GetString(5);
                                studentInfo.course = reader.GetString(6);
                                studentInfo.subjects = reader.GetString(7);
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
            studentInfo.id = Request.Form["id"];
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
                    String sql = "UPDATE Student " +
                        "SET fullname=@fullname, email=@email, mobile_no=@mobile_no, address=@address, gender=@gender, course=@course, subjects=@subjects WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", studentInfo.id);
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
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Student/Index");
        }
    }
}

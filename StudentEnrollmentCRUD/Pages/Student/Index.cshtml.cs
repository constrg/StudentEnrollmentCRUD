using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace StudentEnrollmentCRUD.Pages.Student
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> listStudents = new List<StudentInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=WIN-N74MR2FKADF;Initial Catalog=StudentEnrollmentDb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Student";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studentInfo = new StudentInfo();
                                studentInfo.id = reader.GetInt32(0).ToString();
                                studentInfo.fullname = reader.GetString(1);
                                studentInfo.email = reader.GetString(2);
                                studentInfo.mobile_no = reader.GetString(3);
                                studentInfo.address = reader.GetString(4);
                                studentInfo.gender = reader.GetString(5);
                                studentInfo.course = reader.GetString(6);
                                studentInfo.subjects = reader.GetString(7);
                                studentInfo.created_at = reader.GetDateTime(8).ToString("yyyy-MM-dd HH:mm:ss");
                                listStudents.Add(studentInfo);
                            }
                        }
                    }
                }
                Debug.WriteLine("Number of students retrieved: " + listStudents.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.ToString());
            }
        }
    }

    public class StudentInfo
    {
        public string id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string mobile_no { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string course { get; set; }
        public string subjects { get; set; }
        public string created_at { get; set; }
    }
}

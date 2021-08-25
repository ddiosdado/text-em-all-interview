using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using TextEmAll.Models;

namespace TextEmAll.Controllers
{
    [RoutePrefix("api/school")]
    public class SchoolController : ApiController
    {

        public SchoolController()
        {

        }

        [Route("students")]
        public IEnumerable<StudentGradeInfo> GetStudents()
        {
            string sql = @"
                SELECT gpaInfo.StudentID as StudentId
	                ,p.FirstName
	                ,p.LastName
	                , CONVERT(DECIMAL(10,2),gpaInfo.Gpa) as Gpa
                FROM dbo.Person p
	                CROSS APPLY(
		                SELECT sg.StudentId, (SUM(sg.Grade*c.Credits))/SUM(c.Credits) as Gpa
		                FROM dbo.StudentGrade sg 
			                INNER JOIN dbo.Course c on c.CourseID=SG.CourseID
		                WHERE sg.StudentID=p.PersonID
			                AND sg.Grade IS NOT NULL
		                GROUP BY sg.StudentID
	                )gpaInfo
                WHERE Discriminator='Student'

            ";
            using var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolDb"].ConnectionString);
            var studentInfo = connection.Query<StudentGradeInfo>(sql);
            return studentInfo;
        }
    }
}

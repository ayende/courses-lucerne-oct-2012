using bbv.Models;

namespace bbv.Controllers
{
    public class StudentController : RavenController
    {
         public object New(string firstName, string lastName, string email)
         {
             var student = new Student
                               {
                                   FirstName = firstName,
                                   LastName = lastName,
                                   Email = email
                               };

             Session.Store(student);

             return Json(student.Id);

         }
    }
}
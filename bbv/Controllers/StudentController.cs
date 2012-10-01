using bbv.Models;

namespace bbv.Controllers
{
    using Raven.Client;

    public class StudentController : RavenController
    {
        public object AddSome()
        {
            for (int i = 0; i < 100; i++)
            {
                this.New(
                    "First " + i,
                    "Last " + i,
                    "foo@bar.com");
            }
            return Json("Done");
        }

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
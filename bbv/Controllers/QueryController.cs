using Raven.Client;
using bbv.Models;
using Raven.Client.Linq;
using System.Linq;

namespace bbv.Controllers
{
    public class QueryController : RavenController
    {
         public object GetCourseByName(string name)
         {
             IRavenQueryable<Course> result = Session.Query<Course>().Where(c => c.Name == name);

             return Json(result);
         }

         public object GetUserByHobby(string hobby)
         {
             var result = Session.Query<User>().Where(u => u.Hobbies.Any(h => h == hobby));
             
             return Json(result);
         }

         public object GetStudentByFirstOrLastName(string name)
         {
             IRavenQueryable<Student> result = Session.Query<Student>().Where(s => s.FirstName == name || s.LastName == name);

             return Json(result);
         }

    }
}
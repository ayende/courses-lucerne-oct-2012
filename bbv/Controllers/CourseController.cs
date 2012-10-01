using bbv.Models;

namespace bbv.Controllers
{
	public class CourseController : RavenController
	{
		 public object New(string name)
		 {
			 var course = new Course
				 {
					 Name = name
				 };

			Session.Store(course);

			 return Json(course.Id);
		 }
	}
}
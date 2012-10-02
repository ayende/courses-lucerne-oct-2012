using bbv.Models;

namespace bbv.Controllers
{
	public class MmrController : RavenController
	{
		 public object Create()
		 {
			 var teacher = new Teacher
				 {
					 Name = "Oren Eini"
				 };
			Session.Store(teacher);

			 var course = new Course
				 {
					 Name = "RavenDB",
					 TeacherId = teacher.Id
				 };
			Session.Store(course);

			 for (int i = 0; i < 10; i++)
			 {
				 var student = new Student
					 {
						 FirstName = "Student",
						 LastName = i.ToString(),
						 CourseIds = new[] {course.Id}
					 };
					 Session.Store(student);
			 }
			 return Json("OK");
		 }
	}
}
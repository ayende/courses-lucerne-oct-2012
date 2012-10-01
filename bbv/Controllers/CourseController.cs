using bbv.Models;

namespace bbv.Controllers
{
	public class CourseController : RavenController
	{
		public object Lot()
		{
			for (int i = 0; i < 1000 * 100; i++)
			{
				Session.Store(new Course
					{
						Name = "Course" + i
					});
			}
			return Json("Ouch");
		}

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
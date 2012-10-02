using System.Linq;
using Raven.Client.Indexes;
using bbv.Models;

namespace bbv.Infrastructure.Indexes
{
	public class ClassRoom : AbstractMultiMapIndexCreationTask<ClassRoom.ReduceResult>
	{
		public class ReduceResult
		{
			public string CourseId { get;  set; }
			public string TeacherId { get;  set; }
			public string[] Names { get; set; }
			public int Count { get;  set; }
		}

		public ClassRoom()
		{
			AddMap<Course>(courses =>
						   from course in courses
						   select new
							   {
								  CourseId = course.Id,
								  course.TeacherId,
								  Count = 0,
								  Names = new string[0]
							   });
		
			AddMap<Student>(students =>
			                from student in students 
							from c in student.CourseIds
							select new
								{
									CourseId = c,
									TeacherId = (string)null,
									Count = 1,
									Names = new[]{student.FirstName + " " + student.LastName}
								}
			);

			Reduce = results =>
			         from result in results
			         group result by result.CourseId
			         into g
			         select new
				         {
					         g.FirstOrDefault(x => x.TeacherId != null).TeacherId,
					         CourseId = g.Key,
					         Count = g.Sum(x => x.Count),
							 Names = g.SelectMany(x=>x.Names).Distinct()
				         };

			TransformResults = (database, results) =>
			                   from r in results
			                   select new
				                   {
					                   r.Count,
					                   r.CourseId,
					                   r.Names,
									   r.TeacherId,
									   TeacherName = database.Load<Teacher>(r.TeacherId).Name,
									   CourseName = database.Load<Course>(r.CourseId).Name
				                   };

		}
	}
}
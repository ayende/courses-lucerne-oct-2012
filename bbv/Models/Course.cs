namespace bbv.Models
{
	public class Course
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string TeacherId { get; set; }
	}

	public class Teacher
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}
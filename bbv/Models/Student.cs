﻿namespace bbv.Models
{
    public class Student
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

	    public bool Geek { get; set; }

		public string[] Tags { get; set; }

	    public string[] CourseIds { get; set; }

    }
}
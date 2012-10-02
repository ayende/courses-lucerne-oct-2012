namespace bbv.Models
{
	public class Animal
	{
		public string Name { get; set; }
		public string Species { get; set; }
		public string Breed { get; set; }
		public int Age { get; set; }
		public string[] Tags { get; set; }
	}

    public class Cat : Animal
    {
	    public bool Scratches { get; set; }
    }

    public class Dog : Animal
    {
	    public bool Barks { get; set; }
    }

}
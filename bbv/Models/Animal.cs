namespace bbv.Models
{
	public class Animal
	{
		public string Name { get; set; }
		public string Species { get; set; }
		public string Breed { get; set; }
		public int Age { get; set; }
	}

    public class Cat : Animal
    {
    }

    public class Dog : Animal
    {
    }

}
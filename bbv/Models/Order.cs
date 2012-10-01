namespace bbv.Models
{
	public class Order
	{
		public string Id { get; set; }
		public string CourseId { get; set; }
		public decimal Cost { get; set; }
		public int Participants { get; set; }
	}
}
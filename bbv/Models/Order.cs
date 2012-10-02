namespace bbv.Models
{
	public class Order
	{
		public string Id { get; set; }
		public string CourseId { get; set; }
		public int Participants { get; set; }
		public Line[] Lines { get; set; }

		public class Line
		{
			public string Item { get; set; }
			public decimal Cost { get; set; }
			public int Qty { get; set; }
		}
	}
}
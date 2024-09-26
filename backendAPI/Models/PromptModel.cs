using System.ComponentModel.DataAnnotations;

namespace backendAPI.Models
{
	public class PromptModel
	{
		[Key]
		public int Id { get; set; }
		public string Prompt { get; set; } = null!;
		public DateTime Date { get; set; } = DateTime.Now;
	}
}

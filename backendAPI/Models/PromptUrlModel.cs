using System.ComponentModel.DataAnnotations;

namespace backendAPI.Models
{
	public class PromptUrlModel
	{
		[Key]
		public int Id { get; set; }
		public int PromptId { get; set; }
		public PromptModel? Prompt { get; set; }
		public string? Url { get; set; }
		public string? ErrorMessage { get; set; }
	}
}

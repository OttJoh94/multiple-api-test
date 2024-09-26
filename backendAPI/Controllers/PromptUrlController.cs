using backendAPI.Database;
using backendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PromptUrlController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext context = context;

		[HttpPost]
		public async Task<ActionResult> PostPromptUrl([FromBody] PromptUrlModel promptUrl)
		{
			try
			{
				PromptModel? prompt = await context.Prompts.FindAsync(promptUrl.PromptId);

				if (prompt == null)
				{
					return NotFound("Prompt not found");
				}

				promptUrl.Prompt = prompt;

				await context.PromptUrls.AddAsync(promptUrl);
				await context.SaveChangesAsync();

				return Ok();
			}
			catch
			{
				return BadRequest();
			}

		}

		[HttpGet("{id}")]
		public async Task<ActionResult<PromptUrlModel>> GetPromptUrl(int id)
		{
			try
			{
				PromptUrlModel? promptUrl = await context.PromptUrls.FirstOrDefaultAsync(p => p.PromptId == id);

				if (promptUrl == null)
				{
					return NoContent();
				}
				else
				{
					return Ok(promptUrl);
				}
			}
			catch
			{
				return BadRequest("Something went wrong");
			}
		}
	}
}

using backendAPI.Api;
using backendAPI.Database;
using backendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backendAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PromptController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext context = context;

		// GET: api/<PromptController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PromptModel>>> Get()
		{
			return Ok(await context.Prompts.ToListAsync());
		}

		// GET api/<PromptController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<PromptModel?>> Get(int id)
		{
			PromptModel? prompt = await context.Prompts.FirstOrDefaultAsync(p => p.Id == id);

			if (prompt != null)
			{
				return Ok(prompt);
			}
			else
			{
				return NotFound("Could not find person");
			}
		}


		//Så React kan skicka prompts
		// POST api/<PromptController>
		[HttpPost]
		public async Task<ActionResult<PromptModel>> PostPrompt([FromBody] PromptModel promptToAdd)
		{
			await context.Prompts.AddAsync(promptToAdd);
			await context.SaveChangesAsync();

			//Skicka vidare till python
			PromptModel? result = await new ApiCaller().PostAsync("/generate-prompt", promptToAdd);


			// Svara om det gick bra
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}



		// PUT api/<PromptController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<PromptController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}

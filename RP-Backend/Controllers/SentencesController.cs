using Microsoft.AspNetCore.Mvc;
using RP_Backend.Models.Sentences;
using RP_Backend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP_Backend.Controllers
{
    /// <summary>
    /// Controller which handles adding, deleting, getting sentence. It also contains the Statistics endpoint.
    /// </summary>
    [Route("api/[controller]")]
    public class SentencesController : Controller
    {
        ISentenceService sentences;

        public SentencesController(ISentenceService _sentences)
        {
            sentences = _sentences;
        }

        //GET api/sentences/statistics
        [HttpGet("statistics")]
        public async Task<IEnumerable<StatisticsModel>> Statistics()
        {
            var stats = await sentences.Statistics();

            return stats;
        }

        // GET api/sentences
        [HttpGet]
        public async Task<SentencesGetReturnType> Get(int? page = null)
        {
            return await sentences.Get(page);
        }

        // POST api/sentences
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]SentenceModel model)
        {
            if (ModelState.IsValid)
            {
                await sentences.Create(model);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/sentences/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await sentences.Delete(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

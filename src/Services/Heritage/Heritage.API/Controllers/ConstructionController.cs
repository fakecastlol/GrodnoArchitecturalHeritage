using AutoMapper;
using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Construction.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Heritage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionController : ControllerBase
    {
        private readonly IConstructionService _constructionService;
        private readonly IMapper _mapper;

        public ConstructionController(IConstructionService constructionService, IMapper mapper)
        {
            _constructionService = constructionService;
            _mapper = mapper;
        }

        // GET: api/<ConstructionController>
        [HttpGet("constructions")]
        public async Task<IActionResult> GetAll(int? page, int? pageSize)
        {
            var constructions = await _constructionService.GetUsingPaginationAsync(page ?? 1, pageSize ?? 10);

            return Ok(constructions);
        }

        // GET api/<ConstructionController>/5
        [HttpGet("getconstruction")]
        public async Task<IActionResult> Get([FromQuery] CoreModel model)
        {
            var construction = await _constructionService.GetConstructionByIdAsync(model.Id);

            if (construction == null)
                return BadRequest(new { message = "Construction is not found" });

            return Ok(construction);
        }

        // POST api/<ConstructionController>
        [HttpPost("createconstruction")]
        public async Task<IActionResult> Post([FromBody] ConstructionRequestCoreModel model)
        {
            var result = await _constructionService.CreateConstructionAsync(model);

            return Ok(result);
        }

        // PUT api/<ConstructionController>/5
        [HttpPut("")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConstructionController>/5
        [HttpDelete("deleteconstruction")]
        public async Task Delete(Guid id)
        {
            await _constructionService.DeleteConstructionAsync(id);
        }
    }
}

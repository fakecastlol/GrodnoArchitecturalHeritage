using AutoMapper;
using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Construction.Abstract;
using Heritage.Services.Interfaces.Models.Sorting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Heritage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionController : ControllerBase
    {
        private readonly IConstructionService _constructionService;

        public ConstructionController(IConstructionService constructionService)
        {
            _constructionService = constructionService;
        }

        [HttpGet("constructions")]
        public async Task<IActionResult> GetPaginationAll(Guid? construction, string name, int? page, int? pageSize, SortState sortOrder)
        {
            var constructions = await _constructionService.GetUsingPaginationAsync(construction, name, page ?? 1, pageSize ?? 10, sortOrder);

            return Ok(constructions);
        }


        [HttpGet("getallconstructions")]
        public async Task<IActionResult> GetAll()
        {
            var constructions = await _constructionService.GetAllAsync();

            return Ok(constructions);
        }

        [HttpGet("getconstruction")]
        public async Task<IActionResult> Get([FromQuery] CoreModel model)
        {
            var construction = await _constructionService.GetConstructionByIdAsync(model.Id);

            if (construction == null)
                return BadRequest(new { message = "Construction is not found" });

            return Ok(construction);
        }

        [HttpPost("createconstruction")]
        public async Task<IActionResult> Post([FromBody] ConstructionRequestCoreModel model)
        {
            await _constructionService.CreateConstructionAsync(model);

            return Ok();
        }

        [HttpPost("updateconstruction")]
        public async Task<IActionResult> PostProfile(ConstructionRequestCoreModel model)
        {
            var updateProfile = await _constructionService.UpdateConstructionAsync(model);
            if (model == null)
                return BadRequest(new { message = "User is not found." });

            return Ok(updateProfile);
        }

        [HttpPut("")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("deleteconstruction")]
        public async Task Delete(CoreModel model)
        {
            await _constructionService.DeleteConstructionAsync(model.Id);
        }
    }
}

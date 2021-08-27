using Heritage.API.IntegrationEvents.Enums;
using Heritage.API.IntegrationEvents.EventHandling;
using Heritage.API.IntegrationEvents.Events;
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
        private readonly IEventBus _eventBus;

        public ConstructionController(IConstructionService constructionService, IEventBus eventBus)
        {
            _constructionService = constructionService;
            _eventBus = eventBus;
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
            try
            {
                var createdModel = await _constructionService.CreateConstructionAsync(model);
                var eventMessage = new ConstructionIntegrationEventHandler(new ConstructionIntegrationEvent(DateTime.UtcNow, Actions.Create.ToString(), createdModel.Name, createdModel.Id));

                _eventBus.Publish(eventMessage);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("updateconstruction")]
        public async Task<IActionResult> Update(ConstructionRequestCoreModel model)
        {
            if (model == null)
                return BadRequest(new { message = "Object is not found." });

            var updatedModel = await _constructionService.UpdateConstructionAsync(model);

            try
            {
                var eventMessage = new ConstructionIntegrationEventHandler(new ConstructionIntegrationEvent(DateTime.UtcNow, Actions.Update.ToString(), updatedModel.Name, updatedModel.Id));

                _eventBus.Publish(eventMessage);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(updatedModel);
        }

        [HttpPut("")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("deleteconstruction")]
        public async Task Delete(CoreModel model)
        {
            await _constructionService.DeleteConstructionAsync(model.Id);
            var eventMessage = new ConstructionIntegrationEventHandler(new ConstructionIntegrationEvent(DateTime.UtcNow, Actions.Delete.ToString(), model.Id));

            _eventBus.Publish(eventMessage);
        }
    }
}

using FactoryPatternExample.Model;
using FactoryPatternExample.Service.Handlers;
using FactoryPatternExample.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FactoryPatternExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {

        private readonly UpdateHandlerFactory _factory;

        public UpdateController(UpdateHandlerFactory factory)
        {
            _factory = factory;
        }

        [HttpPut("update/{entityType}")]
        public async Task<IActionResult> Update(string entityType, [FromBody] JsonElement jsonData)
        {
            // Convert entity type string to actual Type
            Type? entityTypeObj = entityType.ToLower() switch
            {
                "user" => typeof(User),
                "product" => typeof(Product),
                _ => null
            };

            if (entityTypeObj == null)
                return BadRequest("Invalid entity type.");

            // Get the correct handler dynamically
            var handlerType = typeof(IUpdateHandler<>).MakeGenericType(entityTypeObj);
            var handler = _factory.GetType()
                                  .GetMethod("GetHandler")?
                                  .MakeGenericMethod(entityTypeObj)
                                  .Invoke(_factory, null);

            if (handler == null)
                return NotFound("No handler found for this entity type.");

            // Deserialize JSON data into the correct entity type
            //var entity = JsonSerializer.Deserialize(jsonData.GetRawText(), entityTypeObj);
            object? entity = JsonSerializer.Deserialize(jsonData.GetRawText(), entityTypeObj, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // 👈 Ensures case-insensitive property matching
            });
            if (entity == null)
                return BadRequest("Invalid data format.");

            // Call the `UpdateAsync` method dynamically
            var task = (Task<bool>)handler.GetType().GetMethod("UpdateAsync")?.Invoke(handler, new[] { entity })!;
            bool success = await task;

            return success ? Ok("Updated successfully.") : NotFound("Update failed.");
        }


        [HttpGet]
        public IActionResult Get()
        {

            return Ok("Hello");
        }
    }
}

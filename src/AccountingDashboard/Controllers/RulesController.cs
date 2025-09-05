namespace AccountingDashboard.Controllers;

using AccountingDashboard.Models;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/rules")]
public class RulesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok();
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        return this.Ok();
    }

    [HttpPost]
    public IActionResult Add([FromBody] AddRuleRequest request)
    {
        return this.NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateRuleRequest request)
    {
        return this.NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return this.NoContent();
    }
}

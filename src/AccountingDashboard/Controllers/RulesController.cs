namespace AccountingDashboard.Controllers;

using AccountingDashboard.Common;
using AccountingDashboard.CQRS.Abstractions;
using AccountingDashboard.Models;

using MediatR;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/rules")]
public class RulesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rules = await mediator.Send(new GetRulesQuery(), this.HttpContext.RequestAborted);
        return this.Ok(rules);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddRuleRequest request)
    {
        var command = new AddRuleCommand
        {
            Client = request.Client!,
            Program = request.Program!,
            DepositDestination = request.DepositDestination!,
        };

        await mediator.Send(command, this.HttpContext.RequestAborted);

        return this.NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRuleRequest request)
    {
        try
        {
            var command = new UpdateRuleCommand
            {
                Id = id,
                Client = request.Client!,
                Program = request.Program!,
                DepositDestination = request.DepositDestination!,
            };

            await mediator.Send(command, this.HttpContext.RequestAborted);

            return this.NoContent();
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteRuleCommand
            {
                Id = id,
            };

            await mediator.Send(command, this.HttpContext.RequestAborted);

            return this.NoContent();
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
    }
}

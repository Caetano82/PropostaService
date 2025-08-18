using Microsoft.AspNetCore.Mvc;
using PropostaService.Application.AlterarStatus;
using PropostaService.Application.CreateProposta;
using PropostaService.Application.GetProposta;
using PropostaService.Application.ListPropostas;
using PropostaService.Domain.ListPropostas;
using Shared.Contracts;

namespace PropostaService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropostasController : ControllerBase
{
    private readonly CreatePropostaHandler _createHandler;
    private readonly ListPropostasHandler _listHandler;
    private readonly GetPropostaHandler _getHandler;
    private readonly AlterarStatusHandler _statusHandler;

    public PropostasController(
        CreatePropostaHandler createHandler,
        ListPropostasHandler listHandler,
        GetPropostaHandler getHandler,
        AlterarStatusHandler statusHandler)
    {
        _createHandler = createHandler;
        _listHandler = listHandler;
        _getHandler = getHandler;
        _statusHandler = statusHandler;
    }

    /// <summary>Cria uma proposta</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePropostaResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePropostaCommand body, CancellationToken ct)
    {
        var result = await _createHandler.HandleAsync(body, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.PropostaId }, result);
    }

    /// <summary>Lista propostas (paginação simples)</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PropostaListItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List([FromQuery] int? take = 50, [FromQuery] int? skip = 0, CancellationToken ct = default)
    {
        var items = await _listHandler.HandleAsync(new ListPropostasQuery(take, skip), ct);
        return Ok(items);
    }

    /// <summary>Obtém uma proposta por Id</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PropostaDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var dto = await _getHandler.HandleAsync(new GetPropostaQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    /// <summary>Altera o status da proposta (EmAnalise, Aprovada, Rejeitada)</summary>
    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] UpdateStatusRequest body, CancellationToken ct)
    {
        try
        {
            await _statusHandler.HandleAsync(new AlterarStatusCommand(id, body.Status), ct);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record UpdateStatusRequest(PropostaStatus Status);
}

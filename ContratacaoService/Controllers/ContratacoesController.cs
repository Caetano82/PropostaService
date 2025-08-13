using ContratacaoService.Application.Contratar;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ContratacoesController : ControllerBase
{
    private readonly ContratarPropostaHandler _handler;

    public ContratacoesController(ContratarPropostaHandler handler) => _handler = handler;

    [HttpPost]
    public async Task<IActionResult> Contratar([FromBody] ContratarPropostaCommand body, CancellationToken ct)
    {
        try
        {
            var r = await _handler.HandleAsync(body, ct);
            return CreatedAtAction(nameof(GetById), new { id = r.ContratoId }, r);
        }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id) => Ok(new { id, message = "implemente consulta se desejar" });
}

using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using Shared.Contracts;

namespace ContratacaoService.Application.ProcessarProposta;

public class ProcessarPropostaHandler
{
    private readonly IContratoRepository _repo;

    public ProcessarPropostaHandler(IContratoRepository repo) => _repo = repo;

    public async Task HandleAsync(PropostaCriada evt, CancellationToken ct)
    {
        var contrato = new Contrato(evt.PropostaId);
        await _repo.AddAsync(contrato, ct);
    }
}

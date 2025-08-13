using PropostaService.Domain.Ports;

namespace PropostaService.Application.GetProposta;

public class GetPropostaHandler
{
    private readonly IPropostaRepository _repo;
    public GetPropostaHandler(IPropostaRepository repo) => _repo = repo;

    public async Task<PropostaDetail?> HandleAsync(GetPropostaQuery q, CancellationToken ct)
    {
        var p = await _repo.GetByIdAsync(q.PropostaId, ct);
        return p is null ? null : new PropostaDetail(p.Id, p.Cliente, p.Valor, p.Status);
    }
}

using PropostaService.Application.ListPropostas;
using PropostaService.Domain.Ports;

namespace PropostaService.Domain.ListPropostas;

public class ListPropostasHandler
{
    private readonly IPropostaRepository _repo;
    public ListPropostasHandler(IPropostaRepository repo) => _repo = repo;

    public async Task<IReadOnlyCollection<PropostaListItem>> HandleAsync(ListPropostasQuery q, CancellationToken ct)
    {
        var list = await _repo.ListAsync(q.Take, q.Skip, ct);
        return list.Select(p => new PropostaListItem(p.Id, p.Cliente, p.Valor, p.Status)).ToList();
    }
}

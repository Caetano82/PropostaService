using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using MassTransit;
using Shared.Contracts;

namespace ContratacaoService.Infrastructure.Adapters;

// Consume PropostaCriada
public class PropostaCriadaConsumer : IConsumer<PropostaCriada>
{
    private readonly IPropostaSnapshotRepository _repo;
    public PropostaCriadaConsumer(IPropostaSnapshotRepository repo) => _repo = repo;

    public async Task Consume(ConsumeContext<PropostaCriada> context)
    {
        var m = context.Message;
        var snap = new PropostaSnapshot
        {
            PropostaId = m.PropostaId,
            Cliente = m.Cliente,
            Valor = m.Valor,
            Status = m.StatusInicial,
            UpdatedAtUtc = m.CreatedAtUtc
        };
        await _repo.UpsertAsync(snap, context.CancellationToken);
    }
}



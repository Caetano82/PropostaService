using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using MassTransit;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratacaoService.Infrastructure.Adapters
{
    // Consume PropostaStatusAlterado
    public class PropostaStatusAlteradoConsumer : IConsumer<PropostaStatusAlterado>
    {
        private readonly IPropostaSnapshotRepository _repo;
        public PropostaStatusAlteradoConsumer(IPropostaSnapshotRepository repo) => _repo = repo;

        public async Task Consume(ConsumeContext<PropostaStatusAlterado> context)
        {
            var m = context.Message;
            var existing = await _repo.GetAsync(m.PropostaId, context.CancellationToken)
                           ?? new PropostaSnapshot { PropostaId = m.PropostaId };

            existing.Status = m.NovoStatus;
            existing.UpdatedAtUtc = m.AlteradoEmUtc;

            await _repo.UpsertAsync(existing, context.CancellationToken);
        }
    }
}

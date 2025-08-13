using ContratacaoService.Application.ProcessarProposta;
using MassTransit;
using Shared.Contracts;

namespace ContratacaoService.Infrastructure.Adapters;

public class PropostaCriadaConsumer : IConsumer<PropostaCriada>
{
    private readonly ProcessarPropostaHandler _handler;
    public PropostaCriadaConsumer(ProcessarPropostaHandler handler) => _handler = handler;

    public async Task Consume(ConsumeContext<PropostaCriada> context)
        => await _handler.HandleAsync(context.Message, context.CancellationToken);
}

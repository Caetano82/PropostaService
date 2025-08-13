using PropostaService.Domain.Entities;
using PropostaService.Domain.Ports;
using Shared.Contracts;

namespace PropostaService.Application.CreateProposta;

public class CreatePropostaHandler
{
    private readonly IPropostaRepository _repo;
    private readonly IEventPublisher _publisher;

    public CreatePropostaHandler(IPropostaRepository repo, IEventPublisher publisher)
    {
        _repo = repo;
        _publisher = publisher;
    }

    public async Task<CreatePropostaResult> HandleAsync(CreatePropostaCommand cmd, CancellationToken ct)
    {
        var proposta = new Proposta(Guid.NewGuid(), cmd.Cliente, cmd.Valor);
        await _repo.AddAsync(proposta, ct);

        var evt = new PropostaCriada(proposta.Id, proposta.Cliente, proposta.Valor, DateTime.UtcNow);
        await _publisher.PublishAsync(evt, ct);

        return new CreatePropostaResult(proposta.Id);
    }
}

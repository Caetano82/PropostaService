using Shared.Contracts;

namespace PropostaService.Application.AlterarStatus;

public record AlterarStatusCommand(Guid PropostaId, PropostaStatus NovoStatus);

namespace Shared.Contracts;
public record PropostaStatusAlterado(
    Guid PropostaId,
    PropostaStatus NovoStatus,
    DateTime AlteradoEmUtc);

namespace Shared.Contracts;

public record PropostaCriada(Guid PropostaId, string Cliente, decimal Valor, DateTime CreatedAtUtc);

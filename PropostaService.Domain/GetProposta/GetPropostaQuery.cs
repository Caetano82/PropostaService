using Shared.Contracts;

namespace PropostaService.Application.GetProposta;

public record GetPropostaQuery(Guid PropostaId);
public record PropostaDetail(Guid Id, string Cliente, decimal Valor, PropostaStatus Status);

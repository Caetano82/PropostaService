namespace PropostaService.Application.CreateProposta;

public record CreatePropostaCommand(string Cliente, decimal Valor);
public record CreatePropostaResult(Guid PropostaId);

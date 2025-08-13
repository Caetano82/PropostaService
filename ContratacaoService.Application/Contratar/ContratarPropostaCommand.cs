namespace ContratacaoService.Application.Contratar;

public record ContratarPropostaCommand(Guid PropostaId);
public record ContratarPropostaResult(Guid ContratoId, Guid PropostaId, DateTime DataContratacaoUtc);

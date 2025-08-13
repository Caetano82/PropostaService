using Shared.Contracts;

namespace PropostaService.Application.ListPropostas;

public record ListPropostasQuery(int? Take = 50, int? Skip = 0);
public record PropostaListItem(Guid Id, string Cliente, decimal Valor, PropostaStatus Status);

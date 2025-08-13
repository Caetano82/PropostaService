namespace ContratacaoService.Domain.Entities;

public class Contrato
{
    public Guid Id { get; private set; }
    public Guid PropostaId { get; private set; }
    public string Status { get; private set; } = "EmAnalise";

    public Contrato(Guid propostaId)
    {
        Id = Guid.NewGuid();
        PropostaId = propostaId;
    }

    public void Aprovar() => Status = "Aprovado";
}

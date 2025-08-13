namespace ContratacaoService.Domain.Entities;

public class Contrato
{
    public Guid Id { get; set; }
    public Guid PropostaId { get; set; }
    public string Status { get; set; } = "EmAnalise";
    public DateTime DataUtc { get; set; } 

    protected Contrato() { }

    public Contrato(Guid propostaId, DateTime? dataUtc = null)
    {
        Id = Guid.NewGuid();
        PropostaId = propostaId;
        DataUtc = dataUtc ?? DateTime.UtcNow;
    }

    public void Aprovar() => Status = "Aprovado";
}

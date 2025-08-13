namespace ContratacaoService.Domain.Entities;

public class Contrato
{
    public Guid Id { get; private set; }
    public Guid PropostaId { get; private set; }
    public DateTime DataContratacaoUtc { get; private set; }
    public string Status { get; private set; } = "Efetuado";

    public Contrato(Guid propostaId, DateTime? dataUtc = null)
    {
        Id = Guid.NewGuid();
        PropostaId = propostaId;
        DataContratacaoUtc = dataUtc ?? DateTime.UtcNow;
    }
}

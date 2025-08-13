using Shared.Contracts;

namespace ContratacaoService.Domain.Entities;

public class PropostaSnapshot
{
    public Guid PropostaId { get; set; }
    public string Cliente { get; set; } = default!;
    public decimal Valor { get; set; }
    public PropostaStatus Status { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}

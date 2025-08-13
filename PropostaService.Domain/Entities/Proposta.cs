using Shared.Contracts;

namespace PropostaService.Domain.Entities;

public class Proposta
{
    public Guid Id { get; private set; }
    public string Cliente { get; private set; } = default!;
    public decimal Valor { get; private set; }
    public PropostaStatus Status { get; private set; }

    public Proposta(Guid id, string cliente, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(cliente))
            throw new ArgumentException("Cliente é obrigatório", nameof(cliente));
        if (valor <= 0)
            throw new ArgumentException("Valor deve ser > 0", nameof(valor));

        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Cliente = cliente.Trim();
        Valor = valor;
        Status = PropostaStatus.EmAnalise;
    }

    public void AlterarStatus(PropostaStatus novo)
    {
        if (Status != PropostaStatus.EmAnalise && novo != Status)
            throw new InvalidOperationException($"Transição inválida de {Status} para {novo}");

        Status = novo;
    }
}

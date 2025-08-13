namespace PropostaService.Domain.Entities;

public class Proposta
{
    public Guid Id { get; private set; }
    public string Cliente { get; private set; } = default!;
    public decimal Valor { get; private set; }
    public string Status { get; private set; } = "Criada";

    public Proposta(Guid id, string cliente, decimal valor)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Cliente = cliente;
        Valor = valor;
    }
}

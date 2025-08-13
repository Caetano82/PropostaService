using System;
using FluentAssertions;
using PropostaService.Domain.Entities;
using Shared.Contracts;
using Xunit;

namespace PropostaService.Tests.Domain;

public class PropostaTests
{
    [Fact]
    public void Should_initialize_with_defaults()
    {
        var p = new Proposta(Guid.Empty, "ACME", 100m);
        p.Id.Should().NotBe(Guid.Empty);
        p.Cliente.Should().Be("ACME");
        p.Valor.Should().Be(100m);
        p.Status.Should().Be(PropostaStatus.EmAnalise);
    }
}

using System;
using ContratacaoService.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ContratacaoService.Tests.Domain;

public class ContratoTests
{
    [Fact]
    public void Should_start_in_analysis_and_allow_approve()
    {
        var c = new Contrato(Guid.NewGuid());
        c.Status.Should().Be("EmAnalise");
        c.Aprovar();
        c.Status.Should().Be("Aprovado");
    }
}

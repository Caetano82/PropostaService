using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PropostaService.Application.CreateProposta;
using PropostaService.Domain.Entities;
using PropostaService.Domain.Ports;
using Shared.Contracts;
using Xunit;

namespace PropostaService.Tests.Application;

public class CreatePropostaHandlerTests
{
    [Fact]
    public async Task Should_create_proposta_and_publish_event()
    {
        // arrange
        var repo = new Mock<IPropostaRepository>();
        repo.Setup(r => r.AddAsync(It.IsAny<Proposta>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var publisher = new Mock<IEventPublisher>();
        publisher.Setup(p => p.PublishAsync(It.IsAny<PropostaCriada>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreatePropostaHandler(repo.Object, publisher.Object);
        var cmd = new CreatePropostaCommand("ACME", 1234.56m);

        // act
        var result = await handler.HandleAsync(cmd, CancellationToken.None);

        // assert
        result.Should().NotBeNull();
        result.PropostaId.Should().NotBe(Guid.Empty);

        repo.Verify(r => r.AddAsync(It.Is<Proposta>(p => p.Cliente == "ACME" && p.Valor == 1234.56m), It.IsAny<CancellationToken>()), Times.Once);
        publisher.Verify(p => p.PublishAsync(It.Is<PropostaCriada>(e => e.Cliente == "ACME" && e.Valor == 1234.56m), It.IsAny<CancellationToken>()), Times.Once);
    }
}

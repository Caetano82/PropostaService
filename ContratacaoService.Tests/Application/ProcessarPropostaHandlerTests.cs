using System.Threading;
using System.Threading.Tasks;
using ContratacaoService.Application.ProcessarProposta;
using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using FluentAssertions;
using Moq;
using Shared.Contracts;
using Xunit;

namespace ContratacaoService.Tests.Application;

public class ProcessarPropostaHandlerTests
{
    [Fact]
    public async Task Should_create_contrato_on_proposta_criada()
    {
        var repo = new Mock<IContratoRepository>();
        repo.Setup(r => r.AddAsync(It.IsAny<Contrato>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new ProcessarPropostaHandler(repo.Object);
        var evt = new PropostaCriada(System.Guid.NewGuid(), "ACME", 500m,PropostaStatus.EmAnalise, System.DateTime.UtcNow);

        await handler.HandleAsync(evt, CancellationToken.None);

        repo.Verify(r => r.AddAsync(It.IsAny<Contrato>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}

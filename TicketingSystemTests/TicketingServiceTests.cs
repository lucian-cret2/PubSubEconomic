using FluentAssertions;
using Messaging;
using Moq;
using TicketingSystem.Domain;
using TicketingSystem.Enums;
using TicketingSystem.Services;

namespace TicketingSystemTests
{
    public class TicketingServiceTests
    {
        private Mock<IPubSub<Purchase>> _mockPubSub = new Mock<IPubSub<Purchase>>();
        private TicketService _sut;
        public TicketingServiceTests() 
        {
            _sut = new TicketService(_mockPubSub.Object);
        }
        [Fact]
        public async void InitiatePurchase_RunsSuccessfully()
        {
            //Arrange
            var purchase = new Purchase()
            {
                RouteName = "routeA",
                State = PurchaseState.NotStarted,
            };

            //Act
            await _sut.InitiatePurchase(purchase);

            //Assert
            purchase.State.Should().Be(PurchaseState.Pending);
            _mockPubSub.Verify(x => x.PublishAsync("trainTickets", "initiatedPurchases", purchase), Times.Once);
        }
    }
}
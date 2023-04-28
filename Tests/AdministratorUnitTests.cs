using bankApp;
using FluentAssertions;
using Newtonsoft.Json.Bson;

namespace bankApp.Tests
{
    [TestClass]
    public class AdministratorUnitTests
    {
        [TestMethod]
        public void AddClient_AddsClient()
        {
            //Arrange
            var bank = new BankSystem();
            var client = new ClientAccount(1);
            var admin = new AdministratorAccount(bank);

            //Act
            admin.AddClient(client);

            //Assert
            bank.ClientList.Count().Should().Be(1);
            client.Bank.Should().Be(bank);
        }

        [TestMethod]
        public void RemoveClient_RemovesClient()
        {
            //Arrange
            var bank = new BankSystem();
            var client1 = new ClientAccount(1);
            var client2 = new ClientAccount(2);
            var admin = new AdministratorAccount(bank);
            admin.AddClient(client1);
            admin.AddClient(client2);

            //Act
            admin.RemoveClient(1);

            //Assert
            bank.ClientList.Count().Should().Be(1);
            bank.ClientList.FirstOrDefault().Should().NotBe(client1);
            client1.Bank.Should().BeNull();
        }

        [TestMethod]
        public void EditClient_EditsClient()
        {
            //Arrange
            var bank = new BankSystem();
            var client = new ClientAccount(1);
            var editedClient = new ClientAccount(2);
            var admin = new AdministratorAccount(bank);

            admin.AddClient(client);

            //Act
            admin.EditClient(1, editedClient);

            //Assert
            bank.ClientList.FirstOrDefault().Bid.Should().Be(editedClient.Bid);
        }

        [TestMethod]
        public void GetOperations_ReturnsList()
        {
            //Arrange
            var bank = new BankSystem();
            var client = new ClientAccount(1);
            var admin = new AdministratorAccount(bank);

            admin.AddClient(client);
            client.Operations.Add(500);
            client.Operations.Add(-200);

            //Act
            var response = admin.GetOperations(1);

            //Assert
            response.Should().NotBeNull();
            response.FirstOrDefault().Should().Be(500);
            response.Count().Should().Be(2);
        }
    }
}
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankApp.Tests
{
    [TestClass]
    public class UserUnitTests
    {
        [TestMethod]
        public void AddFounds_AddsFounds()
        {
            //Arrange
            var client = new ClientAccount(1);

            //Act
            client.AddFounds(500);

            //Assert
            client.Operations.FirstOrDefault().Should().Be(500);
        }

        [TestMethod]
        public void ShowOperations_ReturnsList()
        {
            //Arrange
            var client = new ClientAccount(1);

            client.AddFounds(500);
            client.AddFounds(200);

            //Act
            var response = client.ShowOperations();

            //Assert
            response.Count().Should().Be(2);
            response.FirstOrDefault().Should().Be(500);
        }

        [TestMethod]
        public void TakeLoan_AddsFounds()
        {
            //Arrange
            var client = new ClientAccount(1);

            //Act
            client.TakeLoan(1000);

            //Assert
            client.Operations.FirstOrDefault().Should().Be(1000);
            client.MoneyToPay.Should().Be(1200);
        }

        [TestMethod]
        public void TakeLoan_WhenMoneyAlreadyBorrowed_AddsFounds()
        {
            //Arrange
            var client = new ClientAccount(1);
            client.MoneyToPay = 200;

            //Act
            client.TakeLoan(1000);

            //Assert
            client.Operations.FirstOrDefault().Should().Be(1000);
            client.MoneyToPay.Should().Be(1400);
        }

        [TestMethod]
        public void SendTransfer_WhenNumberTooShort_ReturnsFalse()
        {
            //Arrange
            var bank = new BankSystem();
            var client1 = new ClientAccount(1);
            var client2 = new ClientAccount(2);
            var admin = new AdministratorAccount(bank);

            client2.AccountNumber = "11123412345223453234542345";
            admin.AddClient(client1);
            admin.AddClient(client2);

            //Act
            var response = client1.SendTransfer(100, "111234123452234532345423");

            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public void SendTransfer_WhenWrongAccountNumber_ReturnsFalse()
        {
            //Arrange
            var bank = new BankSystem();
            var client1 = new ClientAccount(1);
            var client2 = new ClientAccount(2);
            var admin = new AdministratorAccount(bank);

            client2.AccountNumber = "11123412345223453234542345";
            admin.AddClient(client1);
            admin.AddClient(client2);

            //Act
            var response = client1.SendTransfer(100, "11223412345223453234542345");

            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public void SendTransfer_WhenWrongReceiverDoesNotExist_ReturnsFalse()
        {
            //Arrange
            var bank = new BankSystem();
            var client1 = new ClientAccount(1);
            var admin = new AdministratorAccount(bank);

            admin.AddClient(client1);

            //Act
            var response = client1.SendTransfer(100, "11123412345223453234542367");

            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public void SendTransfer_ReturnsTrue()
        {
            //Arrange
            var bank = new BankSystem();
            var client1 = new ClientAccount(1);
            var client2 = new ClientAccount(2);
            var admin = new AdministratorAccount(bank);
            long amount = 100;

            client2.AccountNumber = "11123412345223453234542345";
            admin.AddClient(client1);
            admin.AddClient(client2);

            //Act
            var response = client1.SendTransfer(amount, "11123412345223453234542345");

            //Assert
            response.Should().BeTrue();
            client1.Operations.FirstOrDefault().Should().Be(-amount);
            client2.Operations.FirstOrDefault().Should().Be(amount);
        }
    }
}

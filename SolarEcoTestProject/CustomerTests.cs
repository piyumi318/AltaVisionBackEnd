using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEndTestProject
{
    using AltaVision.Logger;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using SolarEcoBackEnd.DataAcessLayer.Interfaces;
    using SolarEcoBackEnd.DB;
    using SolarEcoBackEnd.Entity;

    [TestFixture]
    public class CustomerTests
    {
        private IdFactory factory = new IdFactory();
        Customer customerResult;
        [Test]
        public async Task RegisterCustomer_Success()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Customer");

            var customer = new Customer
            {
                CustomerId = id.getId(),
                FirstName = "Piyumi",
                LastName="Rajapaksha",
                DOB = new DateTime(2000, 3, 18),
                MobileNo="026890303",
                Address="456/b, kandy road, kadawatha",
                Email = "nadimalio@gmail.com",
                Password = "admin@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);

           Customer customerResult = await customerDB.GetCustomerbyId(customer.CustomerId);

            Assert.That(customerResult.FirstName, Is.EqualTo(customer.FirstName));
            Assert.That(customerResult.LastName, Is.EqualTo(customer.LastName));
            Assert.That(customerResult.DOB, Is.EqualTo(customer.DOB));
            Assert.That(customerResult.MobileNo, Is.EqualTo(customer.MobileNo));
            Assert.That(customerResult.Address, Is.EqualTo(customer.Address));
            Assert.That(customerResult.Email, Is.EqualTo(customer.Email));
            Assert.That(customerResult.Password, Is.EqualTo(customer.Password));
            Assert.That(customerResult.StatusId, Is.EqualTo(customer.StatusId));

        }

        [Test]
        public async Task CustomerLogin()
        {
            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Customer");

            var customer = new Customer
            {
                CustomerId = id.getId(),
                FirstName = "Piyumi",
                LastName = "Rajapaksha",
                DOB = new DateTime(2000, 3, 18),
                MobileNo = "026890303",
                Address = "456/b, kandy road, kadawatha",
                Email = "nadimalisadalo123yA@gmail.com",
                Password = "abd@318#",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);
            

           Customer customerResult = await customerDB.CustomerLogin("nadimalisadalo123yA@gmail.com", "abd@318#");


            Assert.That(customerResult.FirstName, Is.EqualTo(customer.FirstName));
            Assert.That(customerResult.LastName, Is.EqualTo(customer.LastName));
            Assert.That(customerResult.DOB, Is.EqualTo(customer.DOB));
            Assert.That(customerResult.MobileNo, Is.EqualTo(customer.MobileNo));
            Assert.That(customerResult.Address, Is.EqualTo(customer.Address));
            Assert.That(customerResult.Email, Is.EqualTo(customer.Email));
            Assert.That(customerResult.Password, Is.EqualTo(customer.Password));
            Assert.That(customerResult.StatusId, Is.EqualTo(customer.StatusId));
        }

        [Test]
        public async Task ExsistCustmer()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Customer");
            var customer = new Customer
            {
                CustomerId = id.getId(),
                FirstName = "Piyumi",
                LastName = "Rajapaksha",
                DOB = new DateTime(2000, 3, 18),
                MobileNo = "026890303",
                Address = "456/b, kandy road, kadawatha",
                Email = "piyu123@gmail.com",
                Password = "abd@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            await customerDB.RegisterCustomer(customer);

            bool result = await customerDB.ExsistCustomer(customer.Email);



            Assert.AreEqual(true, result);

        }
        [Test]
        public async Task ExsistNotCustomer()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);

           

            bool result = await customerDB.ExsistCustomer("piiiiiiiiiiyu123@gmail.com");



            Assert.AreEqual(false, result);


        }
    }
}


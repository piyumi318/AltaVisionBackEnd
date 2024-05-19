using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEndTestProject
{
    using AltaVision.Logger;
    using Castle.Core.Resource;
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
                Email = "piyu7887882h@gmail.com",
                Password = "maDu@318",
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
           

        }
        [Test]
        public async Task RegisterCustomer_FailWithExistEmail()
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
                Email = "piyumi@gmail.com",
                Password = "maDu@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);

            
            Assert.That(result, Is.EqualTo(null));

        }
        [Test]
        public async Task RegisterCustomer_FailWithEmailLengthExceed()
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
                Email = "piyumiMadubashiniRajapaksha@2000318#Piyumi@gmail.com",
                Password = "maDu@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);


            Assert.That(result, Is.EqualTo(null));

        }
        [Test]
        public async Task RegisterCustomer_FailWithCustomerFistNameLegthExceed()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Customer");

            var customer = new Customer
            {
                CustomerId = id.getId(),
                FirstName = "piyumi Madubashini Rajapaksha",
                LastName = "Rajapaksha",
                DOB = new DateTime(2000, 3, 18),
                MobileNo = "026890303",
                Address = "456/b, kandy road, kadawatha",
                Email = "piyumi12@gmail.com",
                Password = "maDu@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);


            Assert.That(result, Is.EqualTo(null));

        }
        [Test]
        public async Task RegisterCustomer_FailWithCustomerLastNameeLegthExceed()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Customer");

            var customer = new Customer
            {
                CustomerId = id.getId(),
                FirstName = "piyumi",
                LastName = "piyumi Madubashini Rajapaksha",
                DOB = new DateTime(2000, 3, 18),
                MobileNo = "026890303",
                Address = "456/b, kandy road, kadawatha",
                Email = "piyumi1234@gmail.com",
                Password = "maDu@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);


            Assert.That(result, Is.EqualTo(null));

        }
        [Test]
        public async Task RegisterCustomer_FailWithCustomerAddresseLegthExceed()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Customer");

            var customer = new Customer
            {
                CustomerId = id.getId(),
                FirstName = "piyumi",
                LastName = "Rajapaksha",
                DOB = new DateTime(2000, 3, 18),
                MobileNo = "026890303",
                Address = "478,B,Obawatta road, Mawaramandiya Siyambalape Kribathgoda,Gampaha Sri Lanka",
                Email = "piyu90@gmail.com",
                Password = "maDu@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);


            Assert.That(result, Is.EqualTo(null));

        }
        [Test]
        public async Task RegisterCustomer_FailWithCustomerMobileNumberLegthExceed()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Customer");

            var customer = new Customer
            {
                CustomerId = id.getId(),
                FirstName = "piyumi",
                LastName = "Rajapaksha",
                DOB = new DateTime(2000, 3, 18),
                MobileNo = "+94071114516789",
                Address = "456/b, kandy road, kadawatha",
                Email = "piyumi09@gmail.com",
                Password = "maDu@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);


            Assert.That(result, Is.EqualTo(null));

        }

        [Test]
        public async Task CustomerLogin_success()
        {
            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            
            

           Customer customerResult = await customerDB.CustomerLogin("piyumi@gmail.com", "maDu@318");

            Assert.That(customerResult.CustomerId, Is.EqualTo("Customer0003"));
            Assert.That(customerResult.FirstName, Is.EqualTo("Piyumi"));
            Assert.That(customerResult.LastName, Is.EqualTo("Rajapaksha"));
            Assert.That(customerResult.Email, Is.EqualTo("piyumi@gmail.com"));
           
           
        }
        [Test]
        public async Task CustomerLogin_FailsByWrongEmail()
        {
            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
           


            Customer customerResult = await customerDB.CustomerLogin("pisdeyumi@gmail.com", "maDu@318");


            Assert.That(customerResult, Is.EqualTo(null));
           
        }

        [Test]
        public async Task CustomerLogin_FailsByWrongPassword()
        {
            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
           


            Customer customerResult = await customerDB.CustomerLogin("piyumi@gmail.com", "maDu@31hsdR");


            Assert.That(customerResult, Is.EqualTo(null));
            
        }
        [Test]
        public async Task CustomerLogin_FailsByWithoutCredentials()
        {
            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);



            Customer customerResult = await customerDB.CustomerLogin(" ", " ");


            Assert.That(customerResult, Is.EqualTo(null));

        }
        [Test]
        public async Task CustomerLogin_FailWithoutEmail()
        {
            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);



            Customer customerResult = await customerDB.CustomerLogin("", "maDu@318");


            Assert.That(customerResult, Is.EqualTo(null));

        }
        [Test]
        public async Task CustomerLogin_FailsWithPassword()
        {
            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);



            Customer customerResult = await customerDB.CustomerLogin("piyumi@gmail.com", " ");


            Assert.That(customerResult, Is.EqualTo(null));

        }


        [Test]
        public async Task ExsistCustmer()
        {

            var loggerMock = new Mock<ILogger<CustomerDB>>();
            var logsMock = new Mock<ILogs>();

            var customerDB = new CustomerDB(loggerMock.Object, logsMock.Object);
            

            bool result = await customerDB.ExsistCustomer("piyu123@gmail.com");



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


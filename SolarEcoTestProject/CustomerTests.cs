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
    using SolarEcoBackEnd.DB;
    using SolarEcoBackEnd.Entity;

    [TestFixture]
    public class CustomerTests
    {
        private IdFactory factory = new IdFactory();
        Customer customerResult;
        [Test]
        public async Task RegisterAdmin_Success()
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
                Email = "admin@gmail.com",
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
                Email = "nadi@gmail.com",
                Password = "abd@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await customerDB.RegisterCustomer(customer);
            

           Customer customerResult = await customerDB.CustomerLogin("nadi@gmail.com", "abd@318");


            Assert.That(customerResult.FirstName, Is.EqualTo(customer.FirstName));
            Assert.That(customerResult.LastName, Is.EqualTo(customer.LastName));
            Assert.That(customerResult.DOB, Is.EqualTo(customer.DOB));
            Assert.That(customerResult.MobileNo, Is.EqualTo(customer.MobileNo));
            Assert.That(customerResult.Address, Is.EqualTo(customer.Address));
            Assert.That(customerResult.Email, Is.EqualTo(customer.Email));
            Assert.That(customerResult.Password, Is.EqualTo(customer.Password));
            Assert.That(customerResult.StatusId, Is.EqualTo(customer.StatusId));
        }

        //    [Test]
        //    public async Task ExsistAdmin()
        //    {

        //        var loggerMock = new Mock<ILogger<AdminDB>>();
        //        var logsMock = new Mock<ILogs>();

        //        var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);

        //        var admin = new Admin
        //        {
        //            AdminName = "Piyumi",
        //            Email = "admin@gmail.com",
        //            Password = "admin@318",
        //            CreatedDate = DateTime.Now,
        //            StatusId = 1
        //        };

        //        bool result = await adminDB.ExsistAdmin("admin@gmail.com");



        //        Assert.AreEqual(true, result);

        //    }
        //    public async Task ExsistNotAdmin()
        //    {

        //        var loggerMock = new Mock<ILogger<AdminDB>>();
        //        var logsMock = new Mock<ILogs>();

        //        var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);



        //        bool result = await adminDB.ExsistAdmin("admgggguiyin@gmail.com");



        //        Assert.AreEqual(false, result);

        //    }
        //}
    }
}

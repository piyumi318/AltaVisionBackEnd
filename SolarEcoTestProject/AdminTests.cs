using AltaVision.Logger;
using Microsoft.Extensions.Logging;
using Moq;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEndTestProject
{
    public class AdminTests

    {
        private IdFactory factory = new IdFactory();
        Admin adminResult;

        [Test]
        public async Task RegisterAdmin_Success()
        {

            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();

            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");

            var admin = new Admin
            {
                AdminId = id.getId(),
                AdminName = "Piyumi",
                Email = "piyu26h@318gmail.com",
                Password = "admin@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await adminDB.RegisterAdmin(admin);

            Admin adminResult = await adminDB.GetAdminbyId(admin.AdminId);

            Assert.That(admin.AdminName, Is.EqualTo(adminResult.AdminName));
            Assert.That(adminResult.Email, Is.EqualTo(admin.Email));
            Assert.That(adminResult.Password, Is.EqualTo(admin.Password));
            Assert.That(adminResult.StatusId, Is.EqualTo(admin.StatusId));

        }
        [Test]
        public async Task RegisterAdmin_FailWithExistEmail()
        {

            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();

            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");

            var admin = new Admin
            {
                AdminId = id.getId(),
                AdminName = "Piyumi",
                Email = "piyumi@318gmail.com",
                Password = "admin@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await adminDB.RegisterAdmin(admin);

           

            Assert.That(result, Is.EqualTo(null));
            
            

        }
        [Test]
        public async Task RegisterAdmin_FailexceedLengthOfEmail()
        {

            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();

            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");

            var admin = new Admin
            {
                AdminId = id.getId(),
                AdminName = "Piyumi",
                Email = "piyumiMadubashiniRajapaksha@2000318#Piyumi@gmail.com",
                Password = "admin@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await adminDB.RegisterAdmin(admin);



            Assert.That(result, Is.EqualTo(null));



        }
        [Test]
        public async Task RegisterAdmin_FailexceedLengthOfAdminName()
        {

            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();

            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");

            var admin = new Admin
            {
                AdminId = id.getId(),
                AdminName = "Kariyapperuma Appuhamilage piyumi Madubashini Rajapaksha",
                Email = "piyumi318madu@gmail.com",
                Password = "admin@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await adminDB.RegisterAdmin(admin);



            Assert.That(result, Is.EqualTo(null));



        }
        [Test]
            public async Task  AdminLogin_Success()
            {
                var loggerMock = new Mock<ILogger<AdminDB>>();
                var logsMock = new Mock<ILogs>();
                var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);

                adminResult = await adminDB.AdminLogin("piyumi@318gmail.com", "admin@318");
           
                Assert.That( adminResult.AdminId, Is.EqualTo("Admin05"));
                Assert.That(adminResult.AdminName, Is.EqualTo("Piyumi"));
                Assert.That(adminResult.Email, Is.EqualTo("piyumi@318gmail.com"));
               
        }
        [Test]
        public async Task AdminLogin_FailByWrongEmail()
        {
            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();
            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");
            
            adminResult = await adminDB.AdminLogin("piyumi@319008gmail.com", "admin@318");

            Assert.That(adminResult, Is.EqualTo(null));
           
        }
        [Test]
        public async Task AdminLogin_FailByWrongPassword()
        {
            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();
            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");
            var admin = new Admin
            {
                AdminId = id.getId(),
                AdminName = "Piyumi",
                Email = "administrator18@gmail.com",
                Password = "administrator1@318#",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };
            await adminDB.RegisterAdmin(admin);
            adminResult = await adminDB.AdminLogin("piyumi@318gmail.com", "admi89jn@318");

            Assert.That(adminResult, Is.EqualTo(null));
        }
        [Test]
        public async Task AdminLogin_FailWithoutCredentials()
        {
            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();
            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");

            adminResult = await adminDB.AdminLogin(" "," ");

            Assert.That(adminResult, Is.EqualTo(null));

        }
        [Test]
        public async Task AdminLogin_FailWihEmptyEmail()
        {
            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();
            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");

            adminResult = await adminDB.AdminLogin("  ", "admin@318");

            Assert.That(adminResult, Is.EqualTo(null));

        }
        [Test]
        public async Task AdminLogin_FailWithEmptyPassword()
        {
            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();
            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
            Id id = factory.CreateId("Admin");

            adminResult = await adminDB.AdminLogin("piyumi@319008gmail.com", " ");

            Assert.That(adminResult, Is.EqualTo(null));

        }

        [Test]
        public async Task ExsistAdmin()
        {

            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();

            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);

            var admin = new Admin
            {
                AdminName = "Piyumi",
                Email = "admin@gmail.com",
                Password = "admin@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };
            await adminDB.RegisterAdmin(admin);

            bool result =await adminDB.ExsistAdmin("admin@gmail.com");

            Assert.AreEqual(true, result);

        }
        [Test]
        public async Task ExsistNotAdmin()
        {

            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();

            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);

            

            bool result = await adminDB.ExsistAdmin("admgggguiyin@gmail.com");



            Assert.AreEqual(false, result);

        }
    }
}

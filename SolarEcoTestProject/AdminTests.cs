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
                Email = "admin@gmail.com",
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
            public async Task  AdminLogin()
            {
                var loggerMock = new Mock<ILogger<AdminDB>>();
                var logsMock = new Mock<ILogs>();
                var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);
                Id id = factory.CreateId("Admin");
                var admin = new Admin
                {
                    AdminId = id.getId(),
                    AdminName = "Piyumi",
                    Email = "admin@122353gmail.com",
                    Password = "admi2332n@318#",
                    CreatedDate = DateTime.Now,
                    StatusId = 1
                };
                await adminDB.RegisterAdmin(admin);
                adminResult = await adminDB.AdminLogin("admin@122353gmail.com", "admi2332n@318#");
           
                Assert.That( adminResult.AdminId, Is.EqualTo(admin.AdminId));
                Assert.That(admin.AdminName, Is.EqualTo(adminResult.AdminName));
                Assert.That(adminResult.Email, Is.EqualTo(admin.Email));
                Assert.That(adminResult.Password, Is.EqualTo(admin.Password));
                Assert.That(adminResult.StatusId, Is.EqualTo(admin.StatusId));
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

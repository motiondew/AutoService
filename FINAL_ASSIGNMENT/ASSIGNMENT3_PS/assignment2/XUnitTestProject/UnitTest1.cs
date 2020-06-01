using System;
using Xunit;
using assignment2;
using assignment2.Models;
using MongoDB.Bson;
using Moq;
using assignment2.Data;
using System.Collections.Generic;
using System.Linq;
using assignment2.Services;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void TestDateEquality()
        {

            var appointmentTest1 = new Appointment
            {
                BsonID = "5ed23976c2555741506bc423",
                date = new DateTime(2020,5,30),
                clientName = "Testy Test",
                telephoneNo = "123456",
                carBrand = "Testla",
                description = "Repair AI ",
                status = 0,
            };

            var repoMock = new Mock<IMongoRepository<Appointment>>();
            repoMock.Setup(s => s.DuplicateBsonId(appointmentTest1)).Returns(true);
            var service = new AppointmentService(repoMock.Object);
            Assert.True(service.Duplicate(appointmentTest1));
        }
    }
}

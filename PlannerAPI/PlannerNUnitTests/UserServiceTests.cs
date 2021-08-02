using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PlannerAPI.Persistence;
using PlannerAPI.Persistence.Repos;
using Microsoft.Extensions.Configuration;
using PlannerAPI.Models.Domain;
using PlannerAPI.Models.Auth;
using System.Collections.Generic;
using PlannerAPI.Services;
using PlannerAPI.Exceptions;


namespace PlannerNUnitTests
{
    public class UserServiceTests
    {

        IOrganizerDao _organizerRepo;

        Organizer _onlyOrganizer;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<PlannerDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            PlannerDbContext newContext = new PlannerDbContext(builder.Options);

            _organizerRepo = new EFOrganizerRepo(newContext);

            newContext.Organizers.RemoveRange(newContext.Organizers);

            _onlyOrganizer = new Organizer
            {
                Name = "Brendan",
                Email = "bdeyo@gmail.com"
            };

            newContext.Organizers.Add(_onlyOrganizer);

            newContext.SaveChanges();
        }

        [Test]
        public void TestGetAllOrganizers()
        {
            Assert.AreEqual(1, _organizerRepo.GetAllOrganizers().Count);
        } 

        [Test]
        public void TestGetOrganizerById()
        {
            Assert.AreEqual(_onlyOrganizer.Id, _organizerRepo.GetOrganizerById(_onlyOrganizer.Id).Id);
        }

    }
}

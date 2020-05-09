using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class GetUniversityAsyncTest
    {
        [Fact]
        public async void GetUniversityAsync_ShouldReturnValidValues()
        {
            //arrange

            var university = new University()
            {
                Id = 1,
                Name = "LNU",
                UserId = 1
            };

            var user = new User()
            {
                Id = 1
            };


            var faculties = new List<Faculty>
            {
                new Faculty() {Id = 1, Name = "LNU"},
                new Faculty() {Id = 2, Name = "NULP"}
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();

            mockUnitOfWork.Setup(u => u.UniversityRepository.GetAsync(university.Id)).ReturnsAsync(university);
            mockUnitOfWork.Setup(u => u.FacultyRepository.GetAllFacultiesWithUniversityId(university.Id)).ReturnsAsync(faculties);

            var universityService = fixture.Create<UniversityService>();


            //Act
            var result = await universityService.GetUniversityAsync(university.Id);

            //Assert

            Assert.True(result.CurrentUniversity == university);
        }
    }
}

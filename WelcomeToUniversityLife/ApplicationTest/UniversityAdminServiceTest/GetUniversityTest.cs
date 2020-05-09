using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.IServices;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class GetUniversityTest
    {
        [Fact]
        public async void GetUniversity_ShouldReturnValidValues()
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
                Id = 1,
                UserName = "username"
            };

            var users = new List<User>
            {
                new User() {Id = 1, UserName = "username"}
            };

            var faculties = new List<Faculty>
            {
                new Faculty() {Id = 1, Name = "Law", UniversityId = 1},
                new Faculty() {Id = 2, Name = "Applied Math", UniversityId = 1}
            };


            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var mockHttpContext = fixture.Freeze<Mock<IHttpContextAccessor>>();
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();

            mockHttpContext.Setup(p => p.HttpContext.User.Identity).Returns(fixture.Create<IIdentity>());
            mockHttpContext.Setup(p => p.HttpContext.User.Identity.Name).Returns("username");

            mockUnitOfWork.Setup(u => u.UniversityRepository.GetUniversityWithUserId(user.Id)).ReturnsAsync(university);
            mockUnitOfWork.Setup(u => u.FacultyRepository.GetAllFacultiesWithUniversityId(university.Id)).ReturnsAsync(faculties);

            mockUserManager.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync(user);


            var universityService = fixture.Create<UniversityService>();

            //Act
            var result = await universityService.GetUniversity();

            //Assert

            Assert.True(result.Faculties == faculties);
        }
    }
}

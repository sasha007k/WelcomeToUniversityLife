using Application.IServices;
using Application.Models.SiteAdmin;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Domain;
using Xunit;
using AutoFixture;
using AutoFixture.AutoMoq;
using Infrastructure.Services;

namespace ApplicationTest.SiteAdminServiceTest
{
    public class AddUniversityTest
    {
        [Fact]
        public async void ShouldAddUniversity()
        {
            //arrange
            var addUniModel = new AddUniversityModel()
            {
                UniversityName = "LNU",
                Email = "test@gmail.com",
                Password = "12345"
            };

            var user = new User
            {
                Id = 1,
                Email = addUniModel.Email,
                UserName = addUniModel.Email
            };

            var university = new University
            {
                Name = addUniModel.UniversityName,
                UserId = user.Id
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            var siteAdminService = fixture.Create<SiteAdminService>();

            var identityResult = fixture.Freeze<IdentityResult>();
            mockUserManager.Setup(u => u.CreateAsync(user, addUniModel.Password))
                .ReturnsAsync(identityResult);

            //act

            await siteAdminService.AddUniversityAsync(addUniModel);

            //assert

            mockUnitOfWork.Verify(unit => unit.UniversityRepository.CreateAsync(It.IsAny<University>()), Times.Once);
        }
    }
}
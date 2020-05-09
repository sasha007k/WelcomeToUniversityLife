using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class DeleteUniversityPhotoTest
    {
        [Fact]
        public async void ShouldDeleteUniversityPhoto()
        {
            //arrange

            var user = new User()
            {
                Id = 1,
                University = new University() {Id = 1, Photo = "test"}
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            mockUnitOfWork.Setup(u => u.UserRepository.GetUserWithUniversityAsync(user.Id)).ReturnsAsync(user);

            var universityService = fixture.Create<UniversityService>();

            //Act

            await universityService.DeleteUniversityPhotoAsync(user.Id);

            //Assert

            mockUnitOfWork.Verify(unit => unit.Commit(), Times.Once);
        }
    }
}

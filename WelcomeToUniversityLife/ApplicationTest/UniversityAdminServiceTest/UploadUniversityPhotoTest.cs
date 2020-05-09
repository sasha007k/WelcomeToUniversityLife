using System;
using System.Collections.Generic;
using System.Text;
using Application.Models.UniversityAdmin;
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
    public class UploadUniversityPhotoTest
    {
        [Fact]
        public async System.Threading.Tasks.Task ShouldThrowException()
        {
            // arrange

            var user = new User()
            {
                Id = 1,
                UserName = "username"
            };

            var model = new UploadPhotoModel()
            {
                id = 1,
                requestedUserId = 1
            };

            var uni = new University()
            {
                Id = 1,
                UserId = 1
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var universityService = fixture.Create<UniversityService>();

            var formCollection = fixture.Create<IFormFileCollection>();
            mockUnitOfWork.Setup(u => u.UniversityRepository.GetUniversityWithUser(model.id)).ReturnsAsync(uni);

            // assert

            await Assert.ThrowsAsync<Exception>(() => universityService.UploadUniversityPhotoAsync(model, formCollection));
        }
    }
}

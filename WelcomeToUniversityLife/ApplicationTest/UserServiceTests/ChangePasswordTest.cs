using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Application.IServices;
using Application.Models.User;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace ApplicationTest.UserServiceTests
{
    public class ChangePasswordTest
    {
        [Fact]
        public async void ShouldChangePassword()
        {
            //arrange

            var model = new ChangePasswordModel()
            {
                OldPassword = "12345",
                NewPassword = "123456",
                ConfirmNewPassword = "123456"
            };

            var user = new User()
            {
                Id = 1,
                UserName = "username",
                Email = "test@gmail.com",
                PasswordHash = "AQAAAAEAACcQAAAAEDt9pmDkZbE2K0S7OqYVfpWcq6IW5m2HGzrRqmTKN35cuZGiKmjsado53ZVIzqNfHA=="
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var mockHttpContext = fixture.Freeze<Mock<IHttpContextAccessor>>();
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            mockHttpContext.Setup(p => p.HttpContext.User.Identity)
                .Returns(fixture.Create<IIdentity>());
            mockHttpContext.Setup(p => p.HttpContext.User.Identity.Name)
                .Returns("username");

            mockUserManager.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync(user);

            var userService = fixture.Create<UserService>();


            //Act
            var result = await userService.ChangePassword(model);

            //Assert

            Assert.False(result);
        }
    }
}

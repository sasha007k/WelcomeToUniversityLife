using System;
using System.Collections.Generic;
using System.Text;
using Application.IServices;
using Application.Models.Authentication;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace ApplicationTest.AuthenticationServiceTest
{
    public class RegisterTest
    {
        [Fact]
        public async void ShouldSignInUser()
        {
            //arrange

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            var mockSignInManager = fixture.Freeze<Mock<ISignInManager>>();

            var registerModel = fixture.Create<RegisterModel>();
            var user = new User()
            {
                Id = 1,
                UserName = "username"
            };

            var roles = new List<string>()
            {
                "User"
            };


            var authenticationService = fixture.Create<AuthenticationService>();

            var identityResult = fixture.Build<IdentityResult>().Create();

            mockUserManager.Setup(u => u.CreateAsync(user, registerModel.Password)).ReturnsAsync(identityResult);


            //Act
            var result = await authenticationService.Register(registerModel);

            //Assert

            Assert.Equal(identityResult.Succeeded, result.Succeeded);
        }
    }
}

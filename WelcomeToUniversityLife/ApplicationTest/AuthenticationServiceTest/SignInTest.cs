using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Application.IServices;
using Application.Models.Authentication;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTest.AuthenticationServiceTest
{
    public class SignInTest
    {
        [Fact]
        public async void ShouldSignInUser()
        {
            //arrange

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            var mockSignInManager = fixture.Freeze<Mock<ISignInManager>>();

            var signInModel = fixture.Create<SignInModel>();
            var user = new User()
            {
                Id = 1,
                UserName = "username"
            };

            var roles = new List<string>()
            {
                "User"
            };

            mockUserManager.Setup(u => u.FindByEmailAsync(signInModel.Email)).ReturnsAsync(user);
            mockUserManager.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(roles);

            mockSignInManager.Setup(u => u.PasswordSignInAsync(user.UserName, signInModel.Password, true, false));

            var authenticationService = fixture.Create<AuthenticationService>();


            //Act
            await authenticationService.SignIn(signInModel);

            //Assert

            mockSignInManager.Verify();
        }
    }
}

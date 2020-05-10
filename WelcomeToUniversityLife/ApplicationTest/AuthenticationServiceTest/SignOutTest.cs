using System;
using System.Collections.Generic;
using System.Text;
using Application.IServices;
using AutoFixture;
using AutoFixture.AutoMoq;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTest.AuthenticationServiceTest
{
    public class SignOutTest
    {
        [Fact]
        public async void ShouldLeaveSystem()
        {
            //arrange

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockSignInManager = fixture.Freeze<Mock<ISignInManager>>();

            var authenticationService = fixture.Create<AuthenticationService>();


            //Act
            await authenticationService.SignOut();

            //Assert

            mockSignInManager.Verify(u => u.SignOutAsync());
        }
    }
}

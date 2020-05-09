using System;
using System.Collections.Generic;
using System.Text;
using Application.IServices;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Moq;
using Xunit;

namespace ApplicationTest.AuthenticationServiceTest
{
    public class FindByEmailTest
    {
        [Fact]
        public async void FindByEmailAsync_ShouldReturnValidValues()
        {
            //arrange

            var user = new User()
            {
                Id = 1,
                Email = "test@gmail.com"
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            mockUserManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            var authenticationService = fixture.Create<AuthenticationService>();


            //Act
            var result = await authenticationService.FindByEmailAsync(user.Email);

            //Assert

            Assert.Equal(user, result);
        }
    }
}

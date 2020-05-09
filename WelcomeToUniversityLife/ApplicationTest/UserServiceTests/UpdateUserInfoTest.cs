using System;
using System.Collections.Generic;
using System.Text;
using Application.IServices;
using Application.Models.User;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTest.UserServiceTests
{
    public class UpdateUserInfoTest
    {
        [Fact]
        public async void GetUserInfo_ShouldReturnValidValues()
        {
            //arrange

            var user = new User()
            {
                Id = 1,
                UserName = "username",
                Email = "test@gmail.com"
            };

            var model = new UserProfileModel()
            {
                Email = user.Email
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            mockUserManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            var userService = fixture.Create<UserService>();


            //Act
            await userService.UpdateUserInfo(model);

            //Assert

            mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<User>()));
        }
    }
}

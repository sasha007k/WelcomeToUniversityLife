﻿using System.Collections.Generic;
using System.Threading;
using Application.IServices;
using Application.Models.User;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ApplicationTest.UserServiceTests
{
    public class GetUserInfoTest
    {
        [Fact]
        public async void GetUserInfo_ShouldReturnValidValues()
        {
            //arrange

            var user = new User()
            {
                Id = 1,
                UserName = "username"
            };

            var model = new UserProfileModel()
            {
                Email = user.Email
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            mockUserManager.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync(user);

            var userService = fixture.Create<UserService>();

            //Act

            var result = await userService.GetUserInfo(user.UserName);

            //Assert

            Assert.Equal(model.Email, result.Email);
        }
    }
}
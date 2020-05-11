﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Application.IServices;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ApplicationTest.UserServiceTests
{
    public class ApplyButtonTest
    {
        [Fact]
        public async void ShouldApplyToSpeciality()
        {
            //arrange

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var mockHttpContext = fixture.Freeze<Mock<IHttpContextAccessor>>();
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            mockHttpContext.Setup(p => p.HttpContext.User.Identity)
                .Returns(fixture.Create<IIdentity>());
            mockHttpContext.Setup(p => p.HttpContext.User.Identity.Name)
                .Returns("username");

            var userService = fixture.Create<UserService>();

            var expected = "Please, set your marks.";

            //Act
            var result = await userService.ApplyButtonExecuteAsync(1);

            //Assert

            Assert.Equal(expected, result);
        }
    }
}
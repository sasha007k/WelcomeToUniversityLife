using System;
using System.Collections.Generic;
using System.Text;
using Application.IServices;
using Application.Models.User;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTest.UserServiceTests
{
    public class AddDocsTest
    {
        [Fact]
        public async void ShouldAddDocs()
        {
            var user = new User()
            {
                Id = 1,
                UserName = "username",
                Documents = new List<Document>()
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            mockUserManager.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync(user);

            var userService = fixture.Create<UserService>();

            var doc = new Document();

            //Act
            await userService.AddDocs("somename", doc);

            //Assert

            mockUserManager.Verify(u => u.UpdateAsync(It.IsAny<User>()));
        }
    }
}

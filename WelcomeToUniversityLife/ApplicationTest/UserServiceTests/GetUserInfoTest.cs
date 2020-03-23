using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Application.IServices;
using Domain.Entities;
using Application.Services;
using System.Threading;
using Application;
using Microsoft.AspNetCore.Http;


namespace ApplicationTest.UserServiceTests
{
    public class GetUserInfoTest
    {
        [Fact]
        void ShouldThrowExceptionWhenUserNotExist()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            using (var context = new DbContext(options))
            {
                var moq = new Mock<IUserStore<User>>();
                moq.Setup(man => man.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(default(User));
                UserManager<User> manager = new UserManager<User>(moq.Object, null, null, null, null, null, null, null, null);
                UserService service = new UserService(manager, null);

                //Act
                var result = service.GetUserInfo("somename");

                //Assert
                Assert.ThrowsAsync<Exception>(() => result);
            }
        }

        [Theory]
        [InlineData("mishka", "misha@gmail.com")]
        [InlineData("kate", "kate@gmail.com")]
        void ShouldRetrieveSameInfo(string name, string email)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()                
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                var moq = new Mock<IUserStore<User>>();
                moq.Setup(s => s.FindByNameAsync(name, CancellationToken.None)).ReturnsAsync(new User() { Email = email });
                UserManager<User> manager = new UserManager<User>(moq.Object, null, null, null, null, null, null, null, null);
                UserService service = new UserService(manager, null);

                //Act
                var result = service.GetUserInfo(name);

                //Assert
                Assert.Equal(email, result.Result.Email);
            }
        }

        [Fact]
        void ShouldInvokeOnceMethodFindByName()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            using (var context = new DbContext(options))
            {
                var moq = new Mock<IUserStore<User>>();
                UserManager<User> manager = new UserManager<User>(moq.Object, null, null, null, null, null, null, null, null);
                UserService service = new UserService(manager, null);

                //Act
                var result = service.GetUserInfo("mishka");

                //Assert
                moq.Verify(u => u.FindByNameAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
            }
        }
    }
}
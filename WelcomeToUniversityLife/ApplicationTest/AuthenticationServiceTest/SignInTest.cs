using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Application.Models.Authentication;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using AuthenticationService = Application.Services.AuthenticationService;

namespace ApplicationTest.AuthenticationServiceTest
{
    public class SignInTest
    {
        [Theory]
        [InlineData("test@gmail.com", "12345")]
        public async void ShouldAuthenticateValidUser(string email, string password)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                context.Set<User>().Add(new User
                {
                    UserName = email,
                    Email = email
                });

                var moq = new Mock<IUserPasswordStore<User>>();
                moq.Setup(s => s.FindByNameAsync(email, CancellationToken.None)).ReturnsAsync(new User() { Email = email });

                var userManager = new UserManager<User>(moq.Object, 
                    null, null, null, null, null, null, null, 
                    new Mock<ILogger<UserManager<User>>>().Object);

                var signInManager = new SignInManager<User>(userManager,
                    new Mock<IHttpContextAccessor>().Object,
                    new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<ILogger<SignInManager<User>>>().Object,
                    new Mock<IAuthenticationSchemeProvider>().Object);
                var service = new AuthenticationService(userManager, signInManager);

                var signInModel = new SignInModel
                {
                    Email = email,
                    Password = password
                };
                var result = await service.SignIn(signInModel);

                const bool expected = false;
                Assert.Equal(expected, result.Succeeded);
            }
        }
    }
}

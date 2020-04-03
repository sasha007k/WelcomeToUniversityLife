using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Application.Models.Authentication;
using Domain;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using AuthenticationService = Infrastructure.Services.AuthenticationService;

namespace ApplicationTest.AuthenticationServiceTest
{
    public class SignUpTest
    {
        [Theory]
        [InlineData("user1@gmail.com", "12345")]
        public async void ShouldRegisterUser(string email, string password)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                context.Set<User>().Add(new User
                {
                    Id = 1,
                    UserName = email,
                    Email = email
                });

                var moq = new Mock<IUserPasswordStore<User>>();
                moq.Setup(man => man.FindByIdAsync("1", CancellationToken.None)).ReturnsAsync(context.Set<User>().FirstOrDefaultAsync(u => u.Id == 1).Result);

                var userManager = new UserManager<User>(moq.Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object);

                var signInManager = new SignInManager<User>(userManager,
                    new Mock<IHttpContextAccessor>().Object,
                    new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<ILogger<SignInManager<User>>>().Object,
                    new Mock<IAuthenticationSchemeProvider>().Object);

                var service = new AuthenticationService(userManager, signInManager);

                var registerModel = new RegisterModel()
                {
                    Email = email,
                    Password = password
                };
                var result = await service.Register(registerModel);

                const bool expected = true;
                Assert.Equal(expected, result.Succeeded);
            }
        }
    }
}

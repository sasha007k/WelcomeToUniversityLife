using System.Threading;
using Application.Models.SiteAdmin;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ApplicationTest.SiteAdminServiceTest
{
    public class AddUniversityTest
    {
        [Theory]
        [InlineData("test@gmail.com", "12345", "LNU")]
        public async void ShouldAddUniversity(string email, string password, string universityName)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("Database")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                //context.Set<User>().Add(new User
                //{
                //    UserName = email,
                //    Email = email
                //});

                var moq = new Mock<IUserPasswordStore<User>>();
                moq.Setup(s => s.FindByNameAsync(email, CancellationToken.None)).ReturnsAsync(new User {Email = email});

                var userManager = new UserManager<User>(moq.Object,
                    null, null, null, null, null, null, null,
                    new Mock<ILogger<UserManager<User>>>().Object);

                var signInManager = new SignInManager<User>(userManager,
                    new Mock<IHttpContextAccessor>().Object,
                    new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<ILogger<SignInManager<User>>>().Object,
                    new Mock<IAuthenticationSchemeProvider>().Object);
                var service = new SiteAdminService(userManager, null, null);

                var addUniversityModel = new AddUniversityModel
                {
                    UniversityName = universityName,
                    Email = email,
                    Password = password
                };
                var result = await service.AddUniversityAsync(addUniversityModel);

                const bool expected = true;
                Assert.Equal(expected, result);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Application.Models.UniversityAdmin;
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
using UniversityAdminService = Infrastructure.Services.UniversityAdminService;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class EditUniversityTest
    {
        [Theory]
        [InlineData("test@gmail.com", "LNU", "Lviv")]
        public async void ShouldEditUniversity(string email, string universityName, string city)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                User user = new User()
                {
                    UserName = email,
                    Email = email
                };
                context.Set<User>().Add(user);
                context.Set<University>().Add(new University
                {
                    Name = universityName,
                    User = user,
                    UserId = user.Id
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

                var httpContext = new HttpContextAccessor();
                var service = new UniversityAdminService(userManager, null, httpContext, null);

                var universityInfoModel = new UniversityInfoModel
                {
                    Name = universityName,
                    City = city
                };
                var result = await service.EditUniversity(universityInfoModel);

                const bool expected = true;
                Assert.Equal(expected, result);
            }
        }
    }
}

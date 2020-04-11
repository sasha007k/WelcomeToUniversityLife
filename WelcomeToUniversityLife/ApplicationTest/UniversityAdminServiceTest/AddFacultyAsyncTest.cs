using System.Threading;
using Application.Models.UniversityAdmin;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class AddFacultyAsyncTest
    {
        [Theory]
        [InlineData("test@gmail.com", "LNU", "Faculty of Applied Mathematics", "Universytetska str", "smth")]
        public async void ShouldAddFacultyAsync(string email, string universityName, string facultyName, string address,
            string description)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("Database")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                var user = new User
                {
                    UserName = email,
                    Email = email
                };
                context.Set<User>().Add(user);
                var university = new University
                {
                    Name = universityName,
                    User = user,
                    UserId = user.Id
                };
                context.Set<University>().Add(university);

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

                var httpContext = new HttpContextAccessor();
                var service = new FacultyService(userManager, null, httpContext, null);
                var addFacultyModel = new AddFacultyModel
                {
                    FacultyName = facultyName,
                    Address = address,
                    Description = description,
                    UniversityId = university.Id
                };
                var result = await service.AddFacultyAsync(addFacultyModel);
                const bool expected = true;
                Assert.Equal(expected, result);
            }
        }
    }
}
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
using UniversityAdminService = Application.Services.UniversityAdminService;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class AddFacultyAsyncTest
    {
        [Theory]
        [InlineData("test@gmail.com", "LNU", "Faculty of Applied Mathematics", "Universytetska str", "smth")]
        public async void ShouldAddFacultyAsync(string email, string universityName, string facultyName, string address, string description)
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
                University university = new University
                {
                    Name = universityName,
                    User = user,
                    UserId = user.Id
                };
                context.Set<University>().Add(university);
                
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
                var service = new UniversityAdminService(userManager, signInManager, context, httpContext);
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

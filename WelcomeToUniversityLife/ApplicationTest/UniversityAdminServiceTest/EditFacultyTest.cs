using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Application.Models.UniversityAdmin;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services.UniversityAdmin;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class EditFacultyTest
    {
        [Fact] 
        public async void ShouldEditFaculty()
        {
            // arrange   

            var faculty = new Faculty()
            {
                Name = "Law Faculty",
                UniversityId = 7
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var mockHttpContext = fixture.Freeze<Mock<IHttpContextAccessor>>();
            mockHttpContext.Setup(p => p.HttpContext.User.Identity)
                .Returns(fixture.Create<IIdentity>());
            mockHttpContext.Setup(p => p.HttpContext.User.Identity.Name)
                .Returns("username");
            var facultyService = fixture.Create<FacultyService>();

            // act

            await facultyService.EditFaculty(faculty);

            // assert

            mockUnitOfWork.Verify(unit => unit.Commit(), Times.Once);
        }
    }
}

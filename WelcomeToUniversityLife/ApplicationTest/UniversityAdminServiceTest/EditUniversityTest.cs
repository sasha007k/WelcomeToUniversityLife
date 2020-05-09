﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class EditUniversityTest
    {
        [Fact]
        public async void ShouldEditUniversity()
        {
            // arrange   

            var university = new University()
            {
                Name = "LNU",
                Id = 7
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var mockHttpContext = fixture.Freeze<Mock<IHttpContextAccessor>>();
            mockHttpContext.Setup(p => p.HttpContext.User.Identity)
                .Returns(fixture.Create<IIdentity>());
            mockHttpContext.Setup(p => p.HttpContext.User.Identity.Name)
                .Returns("username");
            var universityService = fixture.Create<UniversityService>();

            // act

            await universityService.EditUniversity(university);

            // assert

            mockUnitOfWork.Verify(unit => unit.Commit(), Times.Once);
        }
    }
}

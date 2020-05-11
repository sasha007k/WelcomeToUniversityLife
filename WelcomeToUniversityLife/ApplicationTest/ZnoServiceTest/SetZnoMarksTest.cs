using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Application.IServices;
using Application.Models.User;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ApplicationTest.ZnoServiceTest
{
    public class SetZnoMarksTest
    {
        [Fact]
        public async void ShouldSetZnoMarks()
        {
            //arrange

            var user = new User()
            {
                UserName = "username",
                Id = 1
            };

            var model = new AddMarksModel()
            {
                FirstZnoModel = new ZnoModel("English", "200"),
                SecondZnoModel = new ZnoModel("Math", "190"),
                ThirdZnoModel = new ZnoModel("Ukrainian", "195")
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var mockUserManager = fixture.Freeze<Mock<IUserManager>>();
            var mockHttpContext = fixture.Freeze<Mock<IHttpContextAccessor>>();
            mockHttpContext.Setup(p => p.HttpContext.User.Identity)
                .Returns(fixture.Create<IIdentity>());
            mockHttpContext.Setup(p => p.HttpContext.User.Identity.Name)
                .Returns("username");

            var zno = new ZNO()
            {
                English = 200,
                Math = 190,
                Ukrainian = 195
            };

            mockUnitOfWork.Setup(u => u.ZNORepository.CreateAsync(zno));

            mockUserManager.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync(user);

            var znoService = fixture.Create<ZnoService>();

            //Act

           await znoService.SaveZNOMarks(model);

            //Assert

            mockUnitOfWork.Verify(unit => unit.ZNORepository.CreateAsync(It.IsAny<ZNO>()), Times.Once);
        }
    }
}

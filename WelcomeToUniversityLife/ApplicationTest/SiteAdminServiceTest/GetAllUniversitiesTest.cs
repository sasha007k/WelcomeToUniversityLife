using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Moq;
using Xunit;

namespace ApplicationTest.SiteAdminServiceTest
{
    public class GetAllUniversitiesTest
    {
        [Fact]
        public async void GetAllUniversities_ShouldReturnValidValues()
        {
            //arrange

            var universities = new List<University>
            {
                new University{Name = "LNU", Id = 1},
                new University{Name = "NULP", Id = 2}
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();

            mockUnitOfWork.Setup(u => u.UniversityRepository.GetAllUniversitities()).ReturnsAsync(universities);

            var siteAdminService = fixture.Create<SiteAdminService>();


            //Act
            var result = siteAdminService.GetAllUniversities();

            //Assert

            Assert.Equal(universities, result);
        }
    }
}

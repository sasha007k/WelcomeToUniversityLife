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

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class GetFacultyTest
    {
        [Fact]
        public async void GetFacultyAsync_ShouldReturnValidValues()
        {
            //arrange

            University university = new University()
            {
                Id = 1,
                Name = "LNU",
                UserId = 1
            };

            Faculty faculty = new Faculty()
            {
                Id = 1,
                Name = "Fac1",
                UniversityId = 1

            };

            List<Speciality> specialities = new List<Speciality>
            {
                new Speciality{Name = "s1", FacultyId = 1},
                new Speciality{Name = "s2", FacultyId = 1}
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();


            mockUnitOfWork.Setup(u => u.FacultyRepository.GetAsync(faculty.Id)).ReturnsAsync(faculty);
            mockUnitOfWork.Setup(f => f.SpecialityRepository.GetAllSpecialitiesWithFacultyId(faculty.Id)).ReturnsAsync(specialities);
            mockUnitOfWork.Setup(u => u.UniversityRepository.GetUniversityWithId(university.Id)).ReturnsAsync(university);

            var facultyService = fixture.Create<FacultyService>();


            //Act
            var result = await facultyService.GetFacultyAsync(faculty.Id);

            //Assert

            Assert.True(result.CurrentFaculty == faculty);
        }
    }
}

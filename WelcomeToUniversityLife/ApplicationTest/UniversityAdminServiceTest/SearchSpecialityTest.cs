using System;
using System.Collections.Generic;
using System.Text;
using Application.Models.SpecialityModels;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services.UniversityAdmin;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class SearchSpecialityTest
    {
        [Fact]
        public async void GetFacultyAsync_ShouldReturnValidValues()
        {
            //arrange

            var specialities = new List<Speciality>
            {
                new Speciality{Name = "CS", Id = 1, 
                    Faculty = new Faculty()
                    {
                        Name = "Applied Math", 
                        Id = 1,
                        University = new University() {Id = 1, Name = "LNU"}
                    }},
                new Speciality{Name = "CS", Id = 2,
                    Faculty = new Faculty()
                    {
                        Name = "Applied Math",
                        Id = 2,
                        University = new University() {Id = 2, Name = "NULP"}
                    }},
                new Speciality{Name = "CS", Id = 3,
                    Faculty = new Faculty()
                    {
                        Name = "Applied Math",
                        Id = 3,
                        University = new University() {Id = 3, Name = "KPI"}
                    }}
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();


            mockUnitOfWork.Setup(s => s.SpecialityRepository.SearchSpeciality("NU")).ReturnsAsync(specialities);

            var specialityService = fixture.Create<SpecialityService>();


            //Act
            var result = await specialityService.SearchSpecialityAsync("NU");

            //Assert

            var expectedList = new List<SpecialityInfoModel>
            {
                new SpecialityInfoModel{SpecialityName = "CS", id = 1, FacultyName = "Applied Math", UniversityName = "LNU"},
                new SpecialityInfoModel{SpecialityName = "CS", id = 2, FacultyName = "Applied Math", UniversityName = "NULP"},
                new SpecialityInfoModel{SpecialityName = "CS", id = 3, FacultyName = "Applied Math", UniversityName = "KPI"}
            };

            Assert.Equal(expectedList.Count, result.Count);
        }
    }
}

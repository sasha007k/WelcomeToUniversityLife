using System.Collections.Generic;
using Application.Models.UniversityAdmin;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services.UniversityAdmin;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class AddSpecialityTest
    {
        [Fact]
        public async void ShouldAddSpeciality()
        {
            // arrange

            var model = new AddSpecialityModel
            {
                SpecialityName = "Computer Science",
                FreeSpaces = 50,
                PaidSpaces = 20,
                ZNO = new List<string>
                {
                    "Math",
                    "English"
                },
                FacultyId = 1
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var specialityService = fixture.Create<SpecialityService>();

            // act

            await specialityService.AddSpecialityAsync(model);

            // assert

            mockUnitOfWork.Verify(unit => unit.SpecialityRepository.CreateAsync(It.IsAny<Speciality>()), Times.Once);
        }
    }
}

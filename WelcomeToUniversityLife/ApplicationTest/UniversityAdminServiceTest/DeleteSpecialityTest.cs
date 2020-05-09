using System;
using System.Collections.Generic;
using System.Text;
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
    public class DeleteSpecialityTest
    {
        [Fact]
        public async void SaveChanges_shouldInvokeOnce()
        {
            // arrange

            var speciality = new Speciality()
            {
                Id = 7,
                Name = "Computer Science"
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var specialityService = fixture.Create<SpecialityService>();

            // act

            await specialityService.DeleteSpecialityAndSaveAsync(7);

            // assert

            mockUnitOfWork.Verify(unit => unit.Commit(), Times.Once);
        }

        [Fact]
        public async void ShouldDeleteSpeciality()
        {
            // arrange

            var speciality = new Speciality()
            {
                Id = 7,
                Name = "Computer Science"
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var specialityService = fixture.Create<SpecialityService>();

            // act

            await specialityService.DeleteSpecialityAsync(7);

            // assert

            mockUnitOfWork.Verify(x => x.SpecialityRepository.DeleteAsync(7), Times.Once);
        }
    }
}

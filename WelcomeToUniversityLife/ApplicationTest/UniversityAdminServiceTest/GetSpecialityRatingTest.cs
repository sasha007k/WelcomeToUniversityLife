using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services.UniversityAdmin;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class GetSpecialityRatingTest
    {
        [Fact]
        public async void GetSpecialityRatingAsync_ShouldReturnValidValues()
        {
            //arrange

            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var user1 = new User()
            {
                Id = 2,
                Email = "test1@gmaul.com",
                ZNO = new ZNO() { Ukrainian = 200, Math = 190, English = 195}
            };

            var user2 = new User()
            {
                Id = 3,
                Email = "test2@gmaul.com",
                ZNO = new ZNO() { Ukrainian = 200, Math = 190, English = 195 }
            };

            var requests = new List<Request>()
            {
                new Request() {Id = 7, UserId = 2, SpecialityId = 2, User = user1},
                new Request() {Id = 13, UserId = 3, SpecialityId = 2, User = user2}
            };

            var speciality = new Speciality()
            {
                Id = 7,
                Name = "Computer Science"
            };

            
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();


            var specialityService = fixture.Create<SpecialityService>();

            mockUnitOfWork.Setup(u => u.ApplicationRepository.GetAllRequestsBySpecialityId(speciality.Id)).ReturnsAsync(requests);
            mockUnitOfWork.Setup(u => u.SpecialityRepository.GetAsync(speciality.Id)).ReturnsAsync(speciality);

            //Act
            var result = await specialityService.GetSpecialityRatingAsync(speciality.Id);

            //Assert

            Assert.Equal(requests.Count, result.Requests.Count);
        }
    }
}

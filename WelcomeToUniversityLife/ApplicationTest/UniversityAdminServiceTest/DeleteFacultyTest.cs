using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services.UniversityAdmin;
using Moq;
using Xunit;

namespace ApplicationTest.UniversityAdminServiceTest
{
    public class DeleteFacultyTest
    {
        [Fact]
        public async void SaveChanges_shouldInvokeOnce()
        {
            // arrange

            var faculty = new Faculty()
            {
                Name = "Law Faculty",
                UniversityId = 7
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var facultyService = fixture.Create<FacultyService>();

            // act

            await facultyService.DeleteFaculty(7);

            // assert

            mockUnitOfWork.Verify(unit => unit.Commit(), Times.Once);
        }
    }
}

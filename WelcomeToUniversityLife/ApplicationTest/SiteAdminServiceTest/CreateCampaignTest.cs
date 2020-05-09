using System;
using Application.Models.SiteAdmin;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTest.SiteAdminServiceTest
{
    public class CreateCampaignTest
    {
        [Fact]
        public async void ShouldCreateCampaign()
        {
            //arrange

            var model = new CampaignModel()
            {
                Start = new DateTime(2020, 7, 1, 9, 0, 0),
                End = new DateTime(2020, 7, 25, 9, 0, 0)
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();


            var siteAdminService = fixture.Create<SiteAdminService>();

            //Act

            await siteAdminService.CreateCampaignAsync(model);

            //Assert

            mockUnitOfWork.Verify(unit => unit.CampaignRepository.CreateAsync(It.IsAny<Domain.Entities.Сampaign>()), Times.Once);
        }
    }
}

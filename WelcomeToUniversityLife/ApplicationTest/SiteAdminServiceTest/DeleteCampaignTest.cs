using System;
using System.Collections.Generic;
using System.Text;
using Application.Models.SiteAdmin;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTest.SiteAdminServiceTest
{
    public class DeleteCampaignTest
    {
        [Fact]
        public async void ShouldCreateCampaign()
        {
            //arrange

            var campaign = new Domain.Entities.Сampaign()
            {
                Id = 1,
                Start = new DateTime(2020, 7, 1, 9, 0, 0),
                End = new DateTime(2020, 7, 25, 9, 0, 0)
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            mockUnitOfWork.Setup(u => u.CampaignRepository.DeleteAsync(campaign.Id));

            var siteAdminService = fixture.Create<SiteAdminService>();

            //Act

            await siteAdminService.DeleteCampaignAsync(campaign.Id);

            //Assert

            mockUnitOfWork.Verify(unit => unit.CampaignRepository.DeleteAsync(campaign.Id), Times.Once);
        }
    }
}

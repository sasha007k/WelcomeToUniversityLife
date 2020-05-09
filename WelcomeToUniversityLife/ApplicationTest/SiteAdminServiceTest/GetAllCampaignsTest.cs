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
    public class GetAllCampaignsTest
    {
        [Fact]
        public async void ShouldCreateCampaign()
        {
            //arrange

            var campaigns = new List<Domain.Entities.Сampaign>()
            {
                new Domain.Entities.Сampaign()
                {
                    Start = new DateTime(2020, 7, 1, 9, 0, 0),
                    End = new DateTime(2020, 7, 25, 9, 0, 0)
                },
                new Domain.Entities.Сampaign()
                {
                    Start = new DateTime(2020, 7, 1, 9, 0, 0),
                    End = new DateTime(2020, 7, 25, 9, 0, 0)
                }
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();

            var siteAdminService = fixture.Create<SiteAdminService>();
            mockUnitOfWork.Setup(u => u.CampaignRepository.GetAllAsync()).ReturnsAsync(campaigns);


            //Act
            var result = await siteAdminService.GetAllCampaigns();

            //Assert

            Assert.Equal(campaigns.Count, result.Count);
        }
    }
}

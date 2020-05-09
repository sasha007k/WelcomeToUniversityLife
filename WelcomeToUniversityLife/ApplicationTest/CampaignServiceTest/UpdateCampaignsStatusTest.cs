using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Infrastructure.Services;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace ApplicationTest.CampaignServiceTest
{
    public class UpdateCampaignsStatusTest
    {
        [Fact]
        public async void UpdateCampaignsStatus()
        {
            //arrange

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();


            var campaignService = fixture.Create<CampaignService>();


            //Act
            await campaignService.UpdateCampaignsStatus();

            //Assert

            mockUnitOfWork.VerifyAll();
        }
    }
}

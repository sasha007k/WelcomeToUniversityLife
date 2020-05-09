using System;
using System.Collections.Generic;
using System.Text;
using Application.Models.UniversityAdmin;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.UniversityAdmin;
using Moq;
using Xunit;

namespace ApplicationTest.DocumentServiceTest
{
    public class CreateDocumentTest
    {
        [Fact]
        public async void ShouldAddSpeciality()
        {
            // arrange

            var document = new Document()
            {
                Id = 7,
                Name = "ID Card"
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockUnitOfWork = fixture.Freeze<Mock<IUnitOfWork>>();
            var documentService = fixture.Create<DocumentService>();

            // act

            await documentService.Create(document);

            // assert

            mockUnitOfWork.Verify(unit => unit.DocumentRepository.CreateAsync(It.IsAny<Document>()), Times.Once);
        }
    }
}

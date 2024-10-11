using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Tests
{
    public class ImageServiceTest
    {
        public readonly ImageService service;
        private readonly Mock<IImageRepository> repMoq;

        public ImageServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IImageRepository>();

            repositoryWrapperMoq.Setup(x => x.Image)
                .Returns(repMoq.Object);

            service = new ImageService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectImage()
        {
            return new List<object[]>
            {
                new object[] {new Image {ProductId = 1, ImageUrl = "", CreatedBy = 1} },
            };
        }

        [Fact]
        public async void CreateAsync_NullImage_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Image>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewImage_ShouldCreateNewImage()
        {
            var example = new Image()
            {
                ProductId = 1,
                ImageUrl = "imageurl",
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Image>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectImage))]
        public async Task CreateAsync_NewImage_ShouldNotCreateNewImage(Image model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Image>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectImageUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Image { ImageId = 1, ProductId = 1, ImageUrl = "", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, } },
                new object[] {new Image { ImageId = 1, ProductId = 1, ImageUrl = "imageurl", IsDeleted = false, CreatedDate = DateTime.MaxValue, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, } },
                new object[] {new Image { ImageId = 1, ProductId = 1, ImageUrl = "imageurl", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.MaxValue, } },
                new object[] {new Image { ImageId = 1, ProductId = 1, ImageUrl = "imageurl", IsDeleted = true, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new Image { ImageId = 1, ProductId = 1, ImageUrl = "imageurl", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new Image { ImageId = 1, ProductId = 1, ImageUrl = "imageurl", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new Image { ImageId = 1, ProductId = 1, ImageUrl = "imageurl", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullImage_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Image>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewImage_ShouldUpdateNewImage()
        {
            var example = new Image()
            {
                ImageId = 1,
                ProductId = 1,
                ImageUrl = "imageurl",
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Image>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectImageUpdate))]
        public async Task UpdateAsync_NewImage_ShouldNotUpdateNewImage(Image model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Image>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullImage_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Image, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullImage_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Image>()), Times.Never);
        }
    }
}
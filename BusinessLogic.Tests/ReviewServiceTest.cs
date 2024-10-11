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
    public class ReviewServiceTest
    {
        public readonly ReviewService service;
        private readonly Mock<IReviewRepository> repMoq;

        public ReviewServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IReviewRepository>();

            repositoryWrapperMoq.Setup(x => x.Review)
                .Returns(repMoq.Object);

            service = new ReviewService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectReview()
        {
            return new List<object[]>
            {
                new object[] {new Review {  ProductId = 1, UserId = 1,  Rating = 0, Comment = "" } },
                new object[] {new Review {  ProductId = 1, UserId = 1,  Rating = 0, Comment = "comment" } },
                new object[] {new Review {  ProductId = 1, UserId = 1,  Rating = 6, Comment = "comment" } },
                new object[] {new Review {  ProductId = 1, UserId = 1,  Rating = 3, Comment = "" } },
            };
        }

        [Fact]
        public async void CreateAsync_NullReview_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Review>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewReview_ShouldCreateNewReview()
        {
            var example = new Review()
            {
                ProductId = 1,
                UserId = 1,
                Rating = 3,
                Comment = "comment"
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Review>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectReview))]
        public async Task CreateAsync_NewReview_ShouldNotCreateNewReview(Review model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Review>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectReviewUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 0, Comment = "", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 0, Comment = "comment", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 6, Comment = "comment", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 3, Comment = "", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 3, Comment = "comment", IsDeleted = false, CreatedDate = DateTime.MaxValue,  ModifiedDate = DateTime.Now } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 3, Comment = "comment", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.MaxValue } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 3, Comment = "comment", IsDeleted = true, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now, DeletedDate = null, DeletedBy = null } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 3, Comment = "comment", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now, DeletedDate = null, DeletedBy = 1 } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 3, Comment = "comment", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now, DeletedDate = DateTime.Now, DeletedBy = null } },
                new object[] {new Review { ReviewId = 1, ProductId = 1, UserId = 1, Rating = 3, Comment = "comment", IsDeleted = false, CreatedDate = DateTime.Now,  ModifiedDate = DateTime.Now, DeletedDate = DateTime.MaxValue, DeletedBy = 1 } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullReview_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Review>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewReview_ShouldUpdateNewReview()
        {
            var example = new Review()
            {
                ReviewId = 1,
                ProductId = 1,
                UserId = 1,
                Rating = 3,
                Comment = "comment",
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Review>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectReviewUpdate))]
        public async Task UpdateAsync_NewReview_ShouldNotUpdateNewReview(Review model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Review>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullReview_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Review, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullReview_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Review>()), Times.Never);
        }
    }
}
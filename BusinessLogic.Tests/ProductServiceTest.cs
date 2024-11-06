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
    public class ProductServiceTest
    {
        public readonly ProductService service;
        private readonly Mock<IProductRepository> repMoq;

        public ProductServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IProductRepository>();

            repositoryWrapperMoq.Setup(x => x.Product)
                .Returns(repMoq.Object);

            service = new ProductService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectProduct()
        {
            return new List<object[]>
            {
                new object[] {new Product {  ProductName = "",  Description = "", CategoryId = 1, Price = 0, SellerId = 1, CreatedBy = 1} },
                new object[] {new Product {  ProductName = "productname",  Description = "", CategoryId = 1, Price = 30, SellerId = 1, CreatedBy = 1} },
                new object[] {new Product {  ProductName = "",  Description = "description", CategoryId = 1, Price = 30, SellerId = 1, CreatedBy = 1} },
                new object[] {new Product {  ProductName = "productname",  Description = "description", CategoryId = 1, Price = 0, SellerId = 1, CreatedBy = 1} }
            };
        }

        [Fact]
        public async void CreateAsync_NullProduct_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Product>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewProduct_ShouldCreateNewProduct()
        {
            var example = new Product()
            {
                ProductName = "productname",
                Description = "description",
                CategoryId = 1,
                Price = 10,
                SellerId = 1,
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectProduct))]
        public async Task CreateAsync_NewProduct_ShouldNotCreateNewProduct(Product model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Product>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectProductUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Product { ProductId = 1, ProductName = "", Description = "", CategoryId = 1, Price = 0, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now } },
                new object[] {new Product { ProductId = 1, ProductName = "", Description = "description", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "description", CategoryId = 1, Price = 0, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "description", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.MaxValue, ModifiedBy = 1, ModifiedDate = DateTime.Now } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "description", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.MaxValue } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "description", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = true, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "description", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "description", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new Product { ProductId = 1, ProductName = "productname", Description = "description", CategoryId = 1, Price = 2, SellerId = 1, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullProduct_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Product>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewProduct_ShouldUpdateNewProduct()
        {
            var example = new Product()
            {
                ProductId = 1,
                ProductName = "productname",
                Description = "description",
                CategoryId = 1,
                Price = 2,
                SellerId = 1,
                CreatedBy = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectProductUpdate))]
        public async Task UpdateAsync_NewProduct_ShouldNotUpdateNewProduct(Product model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Product>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullProduct_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullProduct_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Product>()), Times.Never);
        }
    }
}
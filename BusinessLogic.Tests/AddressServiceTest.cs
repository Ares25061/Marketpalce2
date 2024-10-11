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
    public class AddressServiceTest
    {
        public readonly AddressService service;
        private readonly Mock<IAdressRepository> repMoq;

        public AddressServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IAdressRepository>();

            repositoryWrapperMoq.Setup(x => x.Adress)
                .Returns(repMoq.Object);

            service = new AddressService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectAddress()
        {
            return new List<object[]>
            {
                new object[] {new Address {UserId = 999, ZipCode = "", AddressLine1 = "", City = "", Country = "", State = "" } },
                new object[] {new Address {UserId = 1, ZipCode = "", AddressLine1 = "address", City = "city", Country = "country", State = "state" } },
                new object[] {new Address {UserId = 1, ZipCode = "zipcode", AddressLine1 = "", City = "city", Country = "country", State = "state" } },
                new object[] {new Address {UserId = 1, ZipCode = "zipcode", AddressLine1 = "address", City = "", Country = "country", State = "state" } },
                new object[] {new Address {UserId = 1, ZipCode = "zipcode", AddressLine1 = "address", City = "city", Country = "", State = "state" } },
                new object[] {new Address {UserId = 1, ZipCode = "zipcode", AddressLine1 = "address", City = "city", Country = "country", State = "" } }
            };
        }

        [Fact]
        public async void CreateAsync_NullAddress_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Address>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewAddress_ShouldCreateNewAddress()
        {
            var example = new Address()
            {
                UserId = 1,
                ZipCode = "zipcode",
                AddressLine1 = "Address",
                City = "city",
                Country = "country",
                State = "state",

            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Address>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectAddress))]
        public async Task CreateAsync_NewAddress_ShouldNotCreateNewAddress(Address model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Address>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectAddressUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "", City = "", Country = "", State = "", ZipCode = "", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now } },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "", City = "city", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now } },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now } },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now} },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now} },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "state", ZipCode = "", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now} },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.MaxValue } },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.MaxValue, ModifiedDate = DateTime.Now } },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null} },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null} },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new Address { AddressId = 1, UserId = 1, AddressLine1 = "address", City = "city", Country = "country", State = "state", ZipCode = "zipcode", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullAddress_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Address>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewAddress_ShouldUpdateNewAddress()
        {
            var example = new Address()
            {
                AddressId = 1,
                UserId = 1,
                AddressLine1 = "address",
                City = "city",
                Country = "country",
                State = "state",
                ZipCode = "zipcode",
                IsDeleted = false,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                DeletedBy = null,
                DeletedDate = null

            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Address>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectAddressUpdate))]
        public async Task UpdateAsync_NewAddress_ShouldNotUpdateNewAddress(Address model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Address>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullAddress_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Address, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullAddress_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Address>()), Times.Never);
        }
    }
}
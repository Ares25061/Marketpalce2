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
    public class FileServiceTest
    {
        public readonly FileService service;
        private readonly Mock<IFileRepository> repMoq;

        public FileServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IFileRepository>();

            repositoryWrapperMoq.Setup(x => x.File)
                .Returns(repMoq.Object);

            service = new FileService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectFile()
        {
            return new List<object[]>
            {
                new object[] {new Domain.Models.File { FileName = "", FilePath = "", FileType = "", FileSize = 1, CreatedBy = 1} },
                new object[] {new Domain.Models.File { FileName = "", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1} },
                new object[] {new Domain.Models.File { FileName = "filename", FilePath = "", FileType = "filetype", FileSize = 1, CreatedBy = 1} },
                new object[] {new Domain.Models.File { FileName = "filename", FilePath = "filepath", FileType = "", FileSize = 1, CreatedBy = 1} },
                new object[] {new Domain.Models.File { FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 0, CreatedBy = 1} },
            };
        }

        [Fact]
        public async void CreateAsync_NullFile_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Domain.Models.File>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewFile_ShouldCreateNewFile()
        {
            var example = new Domain.Models.File()
            {
                FileName = "filename",
                FilePath = "filepath",
                FileType = "filetype",
                FileSize = 1,
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Domain.Models.File>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectFile))]
        public async Task CreateAsync_NewFile_ShouldNotCreateNewFile(Domain.Models.File model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Domain.Models.File>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectFileUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Domain.Models.File { FileId = 1, FileName = "", FilePath = "", FileType = "", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 0, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.MaxValue } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.MaxValue, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = true, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new Domain.Models.File { FileId = 1, FileName = "filename", FilePath = "filepath", FileType = "filetype", FileSize = 1, CreatedBy = 1, ModifiedBy=1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullFile_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Domain.Models.File>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewFile_ShouldUpdateNewFile()
        {
            var example = new Domain.Models.File()
            {
                FileId = 1,
                FileName = "filename",
                FilePath = "filepath",
                FileType = "filetype",
                FileSize = 1,
                CreatedBy = 1,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Domain.Models.File>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectFileUpdate))]
        public async Task UpdateAsync_NewFile_ShouldNotUpdateNewFile(Domain.Models.File model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Domain.Models.File>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullFile_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Domain.Models.File, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullFile_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Domain.Models.File>()), Times.Never);
        }
    }
}
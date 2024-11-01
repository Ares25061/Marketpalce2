using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class ImageService : IImageService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ImageService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Image>> GetAll()
        {

            return await _repositoryWrapper.Image.FindAll();
        }

        public async Task<Image> GetById(int id)
        {
            var image = await _repositoryWrapper.Image
                .FindByCondition(x => x.ImageId == id);
            if (image is null || image.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return image.First();
        }

        public async Task Create(Image model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.ImageUrl))
            {
                throw new ArgumentException(nameof(model.ImageUrl));
            }
           await _repositoryWrapper.Image.Create(model);
           await _repositoryWrapper.Save();
        }

        public async Task Update(Image model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.ImageUrl))
            {
                throw new ArgumentException(nameof(model.ImageUrl));
            }
            if (model.CreatedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.CreatedDate));
            }
            if (model.ModifiedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.ModifiedDate));
            }
            if (model.IsDeleted is true && model.DeletedDate is null || model.DeletedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.IsDeleted));
            }
            if (model.DeletedBy is not null && model.DeletedDate is null || model.DeletedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.DeletedDate));
            }
            if (model.DeletedBy is null && model.DeletedDate is not null)
            {
                throw new ArgumentException(nameof(model.DeletedBy));
            }
           await _repositoryWrapper.Image.Update(model);
           await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var image = await _repositoryWrapper.Image
                .FindByCondition(x => x.ImageId == id);
            if (image is null || image.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
           await _repositoryWrapper.Image.Delete(image.First());
           await _repositoryWrapper.Save();
        }
    }
}
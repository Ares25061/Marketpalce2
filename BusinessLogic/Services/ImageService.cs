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
            return image.First();
        }

        public async Task Create(Image model)
        {
            _repositoryWrapper.Image.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Image model)
        {
            _repositoryWrapper.Image.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var image = await _repositoryWrapper.Image
                .FindByCondition(x => x.ImageId == id);

            _repositoryWrapper.Image.Delete(image.First());
            _repositoryWrapper.Save();
        }
    }
}
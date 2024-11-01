using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class ReviewService : IReviewService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ReviewService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Review>> GetAll()
        {
            return await _repositoryWrapper.Review.FindAll();
        }

        public async Task<Review> GetById(int id)
        {
            var review = await _repositoryWrapper.Review
                .FindByCondition(x => x.ReviewId == id);
            if (review is null || review.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return review.First();
        }

        public async Task Create(Review model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Comment))
            {
                throw new ArgumentException(nameof(model.Comment));
            }
            if (model.Rating < 1 || model.Rating > 5)
            {
                throw new ArgumentException(nameof(model.Rating));
            }
            await _repositoryWrapper.Review.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Review model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Comment))
            {
                throw new ArgumentException(nameof(model.Comment));
            }
            if (model.Rating < 1 || model.Rating > 5)
            {
                throw new ArgumentException(nameof(model.Rating));
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
            await _repositoryWrapper.Review.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var review = await _repositoryWrapper.Review
                .FindByCondition(x => x.ReviewId == id);
            if (review is null || review.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            await _repositoryWrapper.Review.Delete(review.First());
            await _repositoryWrapper.Save();
        }
    }
}
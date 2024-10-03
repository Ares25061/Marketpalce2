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
            return review.First();
        }

        public async Task Create(Review model)
        {
            _repositoryWrapper.Review.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Review model)
        {
            _repositoryWrapper.Review.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var review = await _repositoryWrapper.Review
                .FindByCondition(x => x.ReviewId == id);

            _repositoryWrapper.Review.Delete(review.First());
            _repositoryWrapper.Save();
        }
    }
}
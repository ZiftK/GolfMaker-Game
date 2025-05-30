using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRatingRepository
{
    Task<List<RatingEntity>> GetAll();
    Task<RatingEntity> Create(RatingEntity rating);
    Task<RatingEntity> Update(int id, RatingEntity rating);
    Task Delete(int id);
    Task<RatingEntity> GetById(int id);
    Task<float> GetAverageRatingByLevel(int levelId);
} 
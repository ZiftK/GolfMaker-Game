using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStadisticRepository
{
    Task<List<StadisticsEntity>> GetAll();
    Task<StadisticsEntity> Create(StadisticsEntity stadistic);
    Task<StadisticsEntity> Update(int id, StadisticsEntity stadistic);
    Task Delete(int id);
    Task<StadisticsEntity> GetById(int id);
    Task<List<StadisticsEntity>> GetByUserId(int userId);
    Task<List<StadisticsEntity>> GetByLevelId(int levelId);
    Task<StadisticsEntity> GetByUserAndLevelId(int userId, int levelId);
} 
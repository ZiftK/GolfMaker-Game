using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PrimalLevelRepository : ILevelRepository
{
    private const string LevelDataFilePath = "Assets/Resources/LevelData.json";
    private const string LevelDataBackupFilePath = "Assets/Resources/LevelDataBackup.json";

    public Task<LevelEntity> Create(LevelEntity level)
    {
        throw new System.NotImplementedException();
    }

    public int CreateLevelRecord(LevelEntity level)
    {
        // todo: Implement the logic to create a level record using file system
        string json = JsonConvert.SerializeObject(level);
        System.IO.File.WriteAllText(LevelDataFilePath, json);
        return 1;
    }

    public Task Delete(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<LevelEntity>> GetAll()
    {
        throw new System.NotImplementedException();
    }

    public Task<LevelEntity> GetById(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<LevelEntity>> GetByUserId(int userId)
    {
        throw new System.NotImplementedException();
    }

    public LevelEntity LoadLevelRecord(int id)
    {
        string json = System.IO.File.ReadAllText(LevelDataFilePath);
        LevelEntity level = JsonConvert.DeserializeObject<LevelEntity>(json);
        if (level == null)
        {
            throw new System.Exception("Failed to load level data.");
        }
        

        return level;
    }

    public Task<LevelEntity> Update(int id, LevelEntity level)
    {
        throw new System.NotImplementedException();
    }
}

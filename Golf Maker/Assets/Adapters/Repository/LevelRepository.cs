
public interface ILevelRepository
{
    int CreateLevelRecord( LevelEntity level);
    int[,] LoadLevelRecord(int id);
}
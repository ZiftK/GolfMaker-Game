
public interface ILevelRepository
{
    /// <summary>
    /// Creates a level record in the database.
    /// </summary>
    /// <param name="level"></param>
    /// <returns>
    /// if the level was created successfully, it returns the ID of the created level.
    /// if the level was not created successfully or already exists, it returns -1.
    /// </returns>
    int CreateLevelRecord( LevelEntity level);

    /// <summary>
    /// Loads a level record from the database.
    /// </summary>
    /// <param name="id">level id</param>
    /// <returns>
    /// if the id is valid, it returns the level entity.
    /// if the id is not valid, it returns null.
    /// </returns>
    LevelEntity LoadLevelRecord(int id);
}
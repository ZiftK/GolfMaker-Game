using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILevelRepository
{
    /// <summary>
    /// Gets all levels from the database.
    /// </summary>
    /// <returns>A list of all levels</returns>
    Task<List<LevelEntity>> GetAll();

    /// <summary>
    /// Creates a new level in the database.
    /// </summary>
    /// <param name="level">The level data to create</param>
    /// <returns>The created level with its ID and creation date</returns>
    Task<LevelEntity> Create(LevelEntity level);

    /// <summary>
    /// Updates an existing level in the database.
    /// </summary>
    /// <param name="id">The ID of the level to update</param>
    /// <param name="level">The updated level data</param>
    /// <returns>The updated level</returns>
    Task<LevelEntity> Update(int id, LevelEntity level);

    /// <summary>
    /// Deletes a level from the database.
    /// </summary>
    /// <param name="id">The ID of the level to delete</param>
    Task Delete(int id);

    /// <summary>
    /// Gets all levels created by a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <returns>A list of levels created by the user</returns>
    Task<List<LevelEntity>> GetByUserId(int userId);

    /// <summary>
    /// Gets a specific level by its ID.
    /// </summary>
    /// <param name="id">The ID of the level</param>
    /// <returns>The level if found, null otherwise</returns>
    Task<LevelEntity> GetById(int id);
}
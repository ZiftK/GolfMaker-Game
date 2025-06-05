using UnityEngine;

public class GridIdStorage : MonoBehaviour
{
    private int[,] levelIds;

    private int levelWidth;
    private int levelHeight;

    public void InitGridIdStorage(int levelWidth, int levelHeight)
    {
        this.levelHeight = levelHeight;
        this.levelWidth = levelWidth;
        SetEmptyGrid();
    }

    private void SetEmptyGrid()
    {

        levelIds = new int[levelWidth, levelHeight];

        for (int i = 0; i < levelIds.GetLength(0); i++)
        {
            for (int j = 0; j < levelIds.GetLength(1); j++)
            {
                levelIds[i, j] = -1;
            }
        }
    }
    



    public void SetIdAtPosition(Vector2Int idPosition, int newId)
    {
        if (idPosition.x < 0 || idPosition.x >= levelWidth)
            return;
        if (idPosition.y < 0 || idPosition.y >= levelHeight)
            return;

        levelIds[idPosition.x, idPosition.y] = newId;
    }

    public int GetIdAtPosition(Vector2Int idPosition)
    {
        if (idPosition.x < 0 || idPosition.x >= levelWidth)
            return -1;
        if (idPosition.y < 0 || idPosition.y >= levelHeight)
            return -1;

        return levelIds[idPosition.x, idPosition.y];
    }

    public void SetAllIds(int[,] levelIds) => this.levelIds = levelIds;
    public int[,] GetAllIds() => this.levelIds;

    public string GetParsedStructure() => LevelParser.SerializeLevelIds(levelIds);

    public void SetFromParsedStructure(string serializedStruct)
    {
        int[,] newLevelIds = LevelParser.DeSerializeLevelIds(serializedStruct);

        GridFacade.Instance.LoadTilesFromParseLevel(newLevelIds);
    }
}

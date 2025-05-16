using UnityEngine;
using System.Text;
public class LevelParser
{
    /// <summary>
    /// Serializes a 2D array of integers into a string format.
    /// The format is "row1col1,row1col2,...;row2col1,row2col2,..."
    /// </summary>
    /// <param name="levelIds">The 2D array of integers to serialize.</param>
    /// <returns>A string representation of the 2D array.</returns>
    /// <example>
    /// int[,] levelIds = new int[,] { { 1, 2 }, { 3, 4 } };
    /// string serializedLevel = LevelParser.SerializeLevelIds(levelIds);
    /// serializedLevel will be "1,2;3,4"
    /// </example>
    public static string SerializeLevelIds(int[,] levelIds)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < levelIds.GetLength(0); i++)
        {
            for (int j = 0; j < levelIds.GetLength(1); j++)
            {
                sb.Append(levelIds[i, j]);
                if (j < levelIds.GetLength(1) - 1)
                {
                    sb.Append(",");
                }
            }
            if (i < levelIds.GetLength(0) - 1)
            {
                sb.Append(";");
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Deserializes a string into a 2D array of integers.
    /// The expected format is "row1col1,row1col2,...;row2col1,row2col2,..."
    /// </summary>
    /// <param name="serializedLevel">The string to deserialize.</param>
    /// <returns>A 2D array of integers.</returns>
    /// <example>
    /// string serializedLevel = "1,2;3,4";
    /// int[,] levelIds = LevelParser.DeSerializeLevelIds(serializedLevel);
    /// levelIds will be a 2D array with values { { 1, 2 }, { 3, 4 } }
    /// </example>
    public static int[,] DeSerializeLevelIds(string serializedLevel)
    {
        string[] rows = serializedLevel.Split(';');
        int rowCount = rows.Length;
        int colCount = rows[0].Split(',').Length;

        int[,] levelIds = new int[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            string[] cols = rows[i].Split(',');
            for (int j = 0; j < colCount; j++)
            {
                levelIds[i, j] = int.Parse(cols[j]);
            }
        }
        return levelIds;
    }
}

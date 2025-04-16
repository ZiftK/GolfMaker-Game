using UnityEngine;
using System.Text;
public class MapParser
{
    /// <summary>
    /// Serializes a 2D array of integers into a string format.
    /// The format is "row1col1,row1col2,...;row2col1,row2col2,..."
    /// </summary>
    /// <param name="mapIds">The 2D array of integers to serialize.</param>
    /// <returns>A string representation of the 2D array.</returns>
    /// <example>
    /// int[,] mapIds = new int[,] { { 1, 2 }, { 3, 4 } };
    /// string serializedMap = MapParser.SerializeMapIds(mapIds);
    /// serializedMap will be "1,2;3,4"
    /// </example>
    public static string SerializeMapIds(int[,] mapIds)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < mapIds.GetLength(0); i++)
        {
            for (int j = 0; j < mapIds.GetLength(1); j++)
            {
                sb.Append(mapIds[i, j]);
                if (j < mapIds.GetLength(1) - 1)
                {
                    sb.Append(",");
                }
            }
            if (i < mapIds.GetLength(0) - 1)
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
    /// <param name="serializedMap">The string to deserialize.</param>
    /// <returns>A 2D array of integers.</returns>
    /// <example>
    /// string serializedMap = "1,2;3,4";
    /// int[,] mapIds = MapParser.DeSerializeMapIds(serializedMap);
    /// mapIds will be a 2D array with values { { 1, 2 }, { 3, 4 } }
    /// </example>
    public static int[,] DeSerializeMapIds(string serializedMap)
    {
        string[] rows = serializedMap.Split(';');
        int rowCount = rows.Length;
        int colCount = rows[0].Split(',').Length;

        int[,] mapIds = new int[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            string[] cols = rows[i].Split(',');
            for (int j = 0; j < colCount; j++)
            {
                mapIds[i, j] = int.Parse(cols[j]);
            }
        }
        return mapIds;
    }
}

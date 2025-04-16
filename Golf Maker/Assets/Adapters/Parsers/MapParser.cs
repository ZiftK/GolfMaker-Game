using UnityEngine;
using System.Text;
public class MapParser
{
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

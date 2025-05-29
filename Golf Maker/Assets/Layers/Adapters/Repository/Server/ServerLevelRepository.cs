using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System;
using Newtonsoft.Json;

[Serializable]
public class LevelUpdateData
{
    public string nombre;
    public string dificultad;
    public string descripcion;
    public string estructura_nivel;
    public int cantidad_monedas;
    public int jugado_veces;
    public int completado_veces;
}

public class ServerLevelRepository : ILevelRepository
{
    private static ServerLevelRepository Instance;
    public static ServerLevelRepository GetInstance()
    {
        if (Instance is null)
        {
            Instance = new ServerLevelRepository();
        }
        return Instance;
    }

    private readonly string baseUrl = ServerEnv.levelsServerUrl; // Adjust this URL to match your server

    public async Task<List<LevelEntity>> GetAll()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<List<LevelEntity>>(jsonResponse);
            }
            throw new Exception($"Error getting levels: {request.error}");
        }
    }

    public async Task<LevelEntity> Create(LevelEntity level)
    {
        var createData = new
        {
            id_usuario = level.id_usuario,
            nombre = level.nombre,
            dificultad = level.dificultad,
            descripcion = level.descripcion,
            estructura_nivel = level.estructura_nivel,
            cantidad_monedas = level.cantidad_monedas
        };

        string jsonData = JsonConvert.SerializeObject(createData);
        Debug.Log($"Sending create data: {jsonData}"); // Para debug

        using (UnityWebRequest request = new UnityWebRequest(baseUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error response: {request.downloadHandler.text}"); // Para ver el mensaje de error del servidor
                throw new Exception($"Error creating level: {request.error}");
            }

            string jsonResponse = request.downloadHandler.text;
            return JsonConvert.DeserializeObject<LevelEntity>(jsonResponse);
        }
    }

    public async Task<LevelEntity> Update(int id, LevelEntity level)
    {
        var updateData = new
        {
            nombre = level.nombre,
            dificultad = level.dificultad,
            descripcion = level.descripcion,
            estructura_nivel = level.estructura_nivel,
            cantidad_monedas = level.cantidad_monedas,
            jugado_veces = level.jugado_veces,
            completado_veces = level.completado_veces
        };

        string jsonData = JsonConvert.SerializeObject(updateData);
        Debug.Log($"Sending update data: {jsonData}"); // Para debug

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/{id}", "PUT"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error response: {request.downloadHandler.text}"); // Para ver el mensaje de error del servidor
                throw new Exception($"Error updating level: {request.error}");
            }

            string jsonResponse = request.downloadHandler.text;
            return JsonConvert.DeserializeObject<LevelEntity>(jsonResponse);
        }
    }

    public async Task Delete(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Delete($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Error deleting level: {request.error}");
            }
        }
    }

    public async Task<List<LevelEntity>> GetByUserId(int userId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/usuario/{userId}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<List<LevelEntity>>(jsonResponse);
            }
            throw new Exception($"Error getting user levels: {request.error}");
        }
    }

    public async Task<LevelEntity> GetById(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<LevelEntity>(jsonResponse);
            }
            throw new Exception($"Error getting level: {request.error}");
        }
    }
}

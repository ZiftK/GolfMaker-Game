using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System;

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
        string jsonData = JsonUtility.ToJson(level);
        using (UnityWebRequest request = new UnityWebRequest(baseUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<LevelEntity>(jsonResponse);
            }
            throw new Exception($"Error creating level: {request.error}");
        }
    }

    public async Task<LevelEntity> Update(int id, LevelEntity level)
    {
        string jsonData = JsonUtility.ToJson(level);
        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/{id}", "PUT"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<LevelEntity>(jsonResponse);
            }
            throw new Exception($"Error updating level: {request.error}");
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

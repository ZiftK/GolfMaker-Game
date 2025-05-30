using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System;
using Newtonsoft.Json;

public class ServerStadisticRepository : IStadisticRepository
{
    private static ServerStadisticRepository Instance;
    public static ServerStadisticRepository GetInstance()
    {
        if (Instance is null)
        {
            Instance = new ServerStadisticRepository();
        }
        return Instance;
    }

    private readonly string baseUrl = ServerEnv.stadisticsServerUrl;

    public async Task<List<StadisticsEntity>> GetAll()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<List<StadisticsEntity>>(jsonResponse);
            }
            throw new Exception($"Error getting statistics: {request.error}");
        }
    }

    public async Task<StadisticsEntity> Create(StadisticsEntity stadistic)
    {
        string jsonData = JsonConvert.SerializeObject(stadistic);
        Debug.Log($"Sending create data: {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest(baseUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error response: {request.downloadHandler.text}");
                throw new Exception($"Error creating statistics: {request.error}");
            }

            string jsonResponse = request.downloadHandler.text;
            Debug.Log($"Received JSON: {jsonResponse}");
            return JsonConvert.DeserializeObject<StadisticsEntity>(jsonResponse);
        }
    }

    public async Task<StadisticsEntity> Update(int id, StadisticsEntity stadistic)
    {
        string jsonData = JsonConvert.SerializeObject(stadistic);
        Debug.Log($"Sending update data: {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/{id}", "PUT"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error response: {request.downloadHandler.text}");
                throw new Exception($"Error updating statistics: {request.error}");
            }

            string jsonResponse = request.downloadHandler.text;
            Debug.Log($"Received JSON: {jsonResponse}");
            return JsonConvert.DeserializeObject<StadisticsEntity>(jsonResponse);
        }
    }

    public async Task Delete(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Delete($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Error deleting statistics: {request.error}");
            }
        }
    }

    public async Task<StadisticsEntity> GetById(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<StadisticsEntity>(jsonResponse);
            }
            throw new Exception($"Error getting statistics: {request.error}");
        }
    }

    public async Task<List<StadisticsEntity>> GetByUserId(int userId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/usuario/{userId}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<List<StadisticsEntity>>(jsonResponse);
            }
            throw new Exception($"Error getting user statistics: {request.error}");
        }
    }

    public async Task<List<StadisticsEntity>> GetByLevelId(int levelId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/nivel/{levelId}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<List<StadisticsEntity>>(jsonResponse);
            }
            throw new Exception($"Error getting level statistics: {request.error}");
        }
    }

    public async Task<StadisticsEntity> GetByUserAndLevelId(int userId, int levelId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/usuario/{userId}/nivel/{levelId}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<StadisticsEntity>(jsonResponse);
            }
            throw new Exception($"Error getting user level statistics: {request.error}");
        }
    }
} 
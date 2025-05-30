using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System;
using Newtonsoft.Json;

public class ServerRatingRepository : IRatingRepository
{
    private static ServerRatingRepository Instance;
    public static ServerRatingRepository GetInstance()
    {
        if (Instance is null)
        {
            Instance = new ServerRatingRepository();
        }
        return Instance;
    }

    private readonly string baseUrl = ServerEnv.ratingsServerUrl;

    public async Task<List<RatingEntity>> GetAll()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<List<RatingEntity>>(jsonResponse);
            }
            throw new Exception($"Error getting ratings: {request.error}");
        }
    }

    public async Task<RatingEntity> Create(RatingEntity rating)
    {
        string jsonData = JsonConvert.SerializeObject(rating);
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
                throw new Exception($"Error creating rating: {request.error}");
            }

            string jsonResponse = request.downloadHandler.text;
            Debug.Log($"Received JSON: {jsonResponse}");
            return JsonConvert.DeserializeObject<RatingEntity>(jsonResponse);
        }
    }

    public async Task<RatingEntity> Update(int id, RatingEntity rating)
    {
        string jsonData = JsonConvert.SerializeObject(rating);
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
                throw new Exception($"Error updating rating: {request.error}");
            }

            string jsonResponse = request.downloadHandler.text;
            Debug.Log($"Received JSON: {jsonResponse}");
            return JsonConvert.DeserializeObject<RatingEntity>(jsonResponse);
        }
    }

    public async Task Delete(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Delete($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Error deleting rating: {request.error}");
            }
        }
    }

    public async Task<RatingEntity> GetById(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<RatingEntity>(jsonResponse);
            }
            throw new Exception($"Error getting rating: {request.error}");
        }
    }

    public async Task<float> GetAverageRatingByLevel(int levelId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/nivel/{levelId}/promedio"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Received average rating JSON: {jsonResponse}");
                return JsonConvert.DeserializeObject<float>(jsonResponse);
            }
            throw new Exception($"Error getting average rating for level: {request.error}");
        }
    }
} 
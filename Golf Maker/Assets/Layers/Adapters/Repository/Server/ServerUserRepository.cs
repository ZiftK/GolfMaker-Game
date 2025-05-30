using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System;
using Newtonsoft.Json;

public class ServerUserRepository : IUserRepository
{
    private readonly string baseUrl = ServerEnv.usersServerUrl;

    public static ServerUserRepository Instance;
    public static ServerUserRepository GetInstance()
    {
        if (Instance == null)
        {
            Instance = new ServerUserRepository();
        }
        return Instance;
    }
    public async Task<List<UserEntity>> GetAll()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<List<UserEntity>>(jsonResponse);
            }
            throw new Exception($"Error getting users: {request.error}");
        }
    }

    public async Task<UserEntity> Create(UserEntity user)
    {
        string jsonData = JsonConvert.SerializeObject(user);
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
                return JsonConvert.DeserializeObject<UserEntity>(jsonResponse);
            }
            throw new Exception($"Error creating user: {request.error}");
        }
    }

    public async Task<UserEntity> Update(int id, UserEntity user)
    {
        string jsonData = JsonUtility.ToJson(user);
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
                return JsonUtility.FromJson<UserEntity>(jsonResponse);
            }
            throw new Exception($"Error updating user: {request.error}");
        }
    }

    public async Task Delete(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Delete($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Error deleting user: {request.error}");
            }
        }
    }

    public async Task<UserEntity> GetById(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/{id}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<UserEntity>(jsonResponse);
            }
            throw new Exception($"Error getting user: {request.error}");
        }
    }

    public async Task<UserEntity> GetByUsername(string nombreUsuario)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/nombre/{UnityWebRequest.EscapeURL(nombreUsuario)}"))
        {
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                UserEntity user = JsonConvert.DeserializeObject<UserEntity>(jsonResponse);
                return user;
            }
            if (request.responseCode == 404)
            {
                return null;
            }
            throw new Exception($"Error getting user by username: {request.error}");
        }
    }
}

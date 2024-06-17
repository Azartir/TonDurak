using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LobbyCreator : MonoBehaviour
{
    // URL для создания лобби
    private const string createLobbyUrl = "https://tondurakgame.com/lobbies/create";

    // JWT-токен
    private string jwtToken = "JWT";

    // Параметры лобби
    public string id = "example_id";
    public bool openLobby = true;
    public float bid = 1.0f;
    public int playerAmount = 2;
    public int gameType = 1;
    public string lobbyName = "MyLobby";
    public string password = "myPassword";

    // Метод для создания лобби
    public void CreateLobby()
    {
        StartCoroutine(CreateLobbyCoroutine());
    }

    // Корутина для отправки POST запроса
    private IEnumerator CreateLobbyCoroutine()
    {
        // Создание объекта с параметрами
        var lobbyParams = new Dictionary<string, object>
        {
            { "id", id },
            { "open_lobby", openLobby },
            { "bid", bid },
            { "player_amount", playerAmount },
            { "game_type", gameType },
            { "lobby_name", lobbyName },
            { "password", password }
        };

        // Преобразование параметров в JSON
        string jsonData = JsonConvert.SerializeObject(lobbyParams);

        // Создание запроса
        using (UnityWebRequest www = new UnityWebRequest(createLobbyUrl, "POST"))
        {
            // Установка заголовка с JWT
            www.SetRequestHeader("Authorization", "Bearer " + jwtToken);

            // Установка типа контента и данных
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Ожидание ответа
            yield return www.SendWebRequest();

            // Проверка на ошибки
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Получение и обработка ответа
                string responseText = www.downloadHandler.text;
                Debug.Log("Response: " + responseText);

                // Преобразование ответа из JSON в объект
                var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseText);

                // Обработка данных ответа
                if (response != null)
                {
                    Debug.Log("User ID: " + response["user_id"]);
                    Debug.Log("User Name: " + response["user_name"]);
                    Debug.Log("User Photo: " + response["user_photo"]);
                    if (response.ContainsKey("energy"))
                    {
                        Debug.Log("Energy: " + response["energy"]);
                    }
                    if (response.ContainsKey("balance"))
                    {
                        Debug.Log("Balance: " + response["balance"]);
                    }
                }
            }
        }
    }
}

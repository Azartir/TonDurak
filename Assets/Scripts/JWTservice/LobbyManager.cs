using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LobbyCreator : MonoBehaviour
{
    // URL ��� �������� �����
    private const string createLobbyUrl = "https://tondurakgame.com/lobbies/create";

    // JWT-�����
    private string jwtToken = "JWT";

    // ��������� �����
    public string id = "example_id";
    public bool openLobby = true;
    public float bid = 1.0f;
    public int playerAmount = 2;
    public int gameType = 1;
    public string lobbyName = "MyLobby";
    public string password = "myPassword";

    // ����� ��� �������� �����
    public void CreateLobby()
    {
        StartCoroutine(CreateLobbyCoroutine());
    }

    // �������� ��� �������� POST �������
    private IEnumerator CreateLobbyCoroutine()
    {
        // �������� ������� � �����������
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

        // �������������� ���������� � JSON
        string jsonData = JsonConvert.SerializeObject(lobbyParams);

        // �������� �������
        using (UnityWebRequest www = new UnityWebRequest(createLobbyUrl, "POST"))
        {
            // ��������� ��������� � JWT
            www.SetRequestHeader("Authorization", "Bearer " + jwtToken);

            // ��������� ���� �������� � ������
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // �������� ������
            yield return www.SendWebRequest();

            // �������� �� ������
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // ��������� � ��������� ������
                string responseText = www.downloadHandler.text;
                Debug.Log("Response: " + responseText);

                // �������������� ������ �� JSON � ������
                var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseText);

                // ��������� ������ ������
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

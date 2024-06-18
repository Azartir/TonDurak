using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class UserAuthData
{
    public string user_id;
}

public class JWTTaker : MonoBehaviour
{
    private const string url = "https://tondurakgame.com/gameauth";
    public static string token = string.Empty;

    void Start()
    {
        StartCoroutine(PostRequest());
    }

    IEnumerator PostRequest()
    {
        UserAuthData userData = new UserAuthData();
        userData.user_id = "1234124";


        string jsonData = JsonConvert.SerializeObject(userData);

        UnityWebRequest www = new UnityWebRequest(url + $"?user_id={userData.user_id}", "POST")
        {
            uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        // ��������� ����������
        www.SetRequestHeader("accept", "application/json");

        // �������� ������� � �������� ������
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("������: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("����� �������: " + responseText);

            JObject jsonResponse = JObject.Parse(responseText);
            token = jsonResponse["accessToken"]?.ToString();

            // ������� ����������� �������� accessToken � ���
            if (token != null)
            {
                Debug.Log("����������� accessToken: " + token);
            }
            else
            {
                Debug.LogError("���� 'accessToken' �� ������ � ������ �������.");
            }
        }
    }
}

using UnityEngine;
using Photon.Pun;

public class PhotonTimer : MonoBehaviourPun
{
    public delegate void TimerFinishedAction();
    public static event TimerFinishedAction OnTimerFinished;

    private float timer = 0f;
    [SerializeField] private float maxTime = 60f;
    private bool timerRunning = true;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Если вы хотите, чтобы первый клиент, который создает комнату, контролировал время
            // Иначе, вы можете инициализировать время на всех клиентах с помощью RPC (Remote Procedure Call) при подключении новых игроков
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && timerRunning)
        {
            timer += Time.deltaTime;

            if (timer >= maxTime)
            {
                timerRunning = false;
                OnTimerFinish(); // Вызываем метод по окончанию работы таймера
            }

            // Отправить текущее время другим игрокам
            photonView.RPC("SyncTimer", RpcTarget.Others, timer);
        }
    }

    [PunRPC]
    void SyncTimer(float newTime)
    {
        timer = newTime;
    }

    void OnTimerFinish()
    {
        // Действия по завершению работы таймера
        Debug.Log("Timer finished!");
        // Вызываем событие, которое может быть подписано другими объектами
        OnTimerFinished?.Invoke();
    }
}

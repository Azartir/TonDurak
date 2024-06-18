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
            // ���� �� ������, ����� ������ ������, ������� ������� �������, ������������� �����
            // �����, �� ������ ���������������� ����� �� ���� �������� � ������� RPC (Remote Procedure Call) ��� ����������� ����� �������
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
                OnTimerFinish(); // �������� ����� �� ��������� ������ �������
            }

            // ��������� ������� ����� ������ �������
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
        // �������� �� ���������� ������ �������
        Debug.Log("Timer finished!");
        // �������� �������, ������� ����� ���� ��������� ������� ���������
        OnTimerFinished?.Invoke();
    }
}

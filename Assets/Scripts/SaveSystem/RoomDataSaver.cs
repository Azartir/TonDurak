using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SaveSystem
{
    [Serializable]
    public class RoomDataList
    {
        public List<RoomData> Rooms = new List<RoomData>();
    }

    public class RoomDataSaver : MonoBehaviour
    {
        public static RoomDataSaver Singleton { get; set; }

        private RoomDataList _rooms = new RoomDataList();
        private string _jsonName = "RoomData.json";

        private string _path;

        private void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _path = Path.Combine(Application.persistentDataPath, _jsonName);
            if (!File.Exists(_path)) return;
            _rooms = LoadRoomDataListFromJson(_path);
        }

        public void RemoveRoom(RoomData room)
        {
            if (!_rooms.Rooms.Contains(room)) return;
            _rooms.Rooms.Remove(room);
            SaveRoomDataListToJson(_rooms.Rooms, _path);
        }

        public void AddRoom(RoomData room)
        {
            Debug.Log("Room added");
            if (_rooms.Rooms.Contains(room)) return;
            _rooms.Rooms.Add(room);
            SaveRoomDataListToJson(_rooms.Rooms, _path);
        }

        public RoomData GetRoomDataById(uint id)
        {
            foreach (var room in _rooms.Rooms)
            {
                if (room.Id == id)
                    return room;
            }

            return new RoomData();
        }
        
        private static void SaveRoomDataListToJson(List<RoomData> roomDataList, string filePath)
        {
            RoomDataList dataList = new RoomDataList { Rooms = roomDataList };
            string json = JsonUtility.ToJson(dataList, true);
            File.WriteAllText(filePath, json);
        }

        private static RoomDataList LoadRoomDataListFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string json = File.ReadAllText(filePath);
            RoomDataList dataList = JsonUtility.FromJson<RoomDataList>(json);
            return dataList;
        }
    }
}
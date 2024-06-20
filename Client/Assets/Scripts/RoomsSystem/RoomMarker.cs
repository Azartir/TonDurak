using UnityEngine;

namespace RoomsSystem
{
    public class RoomMarker : MonoBehaviour
    {
        public static RoomMarker Singleton { get; set; }

        public int UsersCount { get; set; }

        private void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
                return;
            }
            Singleton = this;
        }
    }
}
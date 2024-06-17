using SaveSystem;
using UnityEngine;

namespace GameSystem
{
    public class GameTypeMarker : MonoBehaviour
    {
        public static GameTypeMarker Singleton { get; set; }

        [SerializeField] private GameType _gameType;
        public GameType GameType => _gameType;

        private void Awake()
            => Singleton = this;
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LeaderBoardHandler : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerScore> _playerScores = new List<PlayerScore> {
            new PlayerScore { time = 20.581f, name = "Inkizd" },
            new PlayerScore { time = 17.022f, name = "Msv"},
            new PlayerScore { time = 13.689f, name = "Lambb"}
        };

        [SerializeField]
        private TextMeshProUGUI _tmp;

        private void Start()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            var _gameTime = Resources.Load<FloatVariable>("ScriptableObjects/GameTime");
            var userScore = new PlayerScore { name = "You", time = _gameTime.Value };
            _playerScores.Add(userScore);
            _playerScores.Sort((x, y) => x.time.CompareTo(y.time));

            _tmp.text = "";

            foreach(var score in _playerScores)
            {
                var minutes = ((int)(score.time / 60)).ToString().PadLeft(2, '0');
                var seconds = ((int)(score.time % 60)).ToString().PadLeft(2, '0');
                var milliseconds = ((int)((score.time * 1000) % 1000)).ToString().PadLeft(3, '0');

                _tmp.text += $"{score.name} - {minutes}:{seconds}:{milliseconds}<br>";
            }
        }


    }

    public struct PlayerScore
    {
        public float time;
        public string name;
    }
}
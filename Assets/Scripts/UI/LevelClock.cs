using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LevelClock : MonoBehaviour
    {
        private FloatVariable _gameTime;
        private TextMeshProUGUI _tmp;

        private void Start()
        {
            _gameTime = Resources.Load<FloatVariable>("ScriptableObjects/GameTime");
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            var minutes = ((int) (_gameTime.Value / 60)).ToString().PadLeft(2,'0');
            var seconds = ((int) (_gameTime.Value % 60)).ToString().PadLeft(2, '0');
            var milliseconds = ((int)((_gameTime.Value * 1000) % 1000)).ToString().PadLeft(3, '0');


            _tmp.text = $"{minutes}:{seconds}:{milliseconds}";
        }
    }
}
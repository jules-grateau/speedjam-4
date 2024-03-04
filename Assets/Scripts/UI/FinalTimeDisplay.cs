using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FinalTimeDisplay : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable _gameTime;
        private TextMeshProUGUI _tmp;
        [SerializeField]
        private string _finalText;

        private void Start()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            var minutes = ((int)(_gameTime.Value / 60)).ToString().PadLeft(2, '0');
            var seconds = ((int)(_gameTime.Value % 60)).ToString().PadLeft(2, '0');
            var milliseconds = ((int)((_gameTime.Value * 1000) % 1000)).ToString().PadLeft(3, '0');

            _tmp.text = _finalText + $"{minutes}:{seconds}:{milliseconds}";
        }


    }
}
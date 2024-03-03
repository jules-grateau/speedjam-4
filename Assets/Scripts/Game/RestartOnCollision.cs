using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    public class RestartOnCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
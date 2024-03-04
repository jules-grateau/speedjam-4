using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField]
        private Transform _destination;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.transform.position = _destination.position;
            }
        }
    }
}
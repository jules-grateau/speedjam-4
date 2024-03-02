using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedDisplayController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private Rigidbody2D _playerRigidBody;
    private TextMeshProUGUI _tmp;
    // Start is called before the first frame update
    void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _playerRigidBody = _player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _tmp.text = "Speed : " + Mathf.Abs(_playerRigidBody.velocity.x);
    }
}

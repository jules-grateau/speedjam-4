using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCart : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private CinemachineDollyCart _cart;
    // Start is called before the first frame update
    void Start()
    {
        _cart = GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    { 
        _cart.m_Position = _cart.m_Path.FindClosestPoint(_player.transform.position, 0, -1, 10);
    }
}

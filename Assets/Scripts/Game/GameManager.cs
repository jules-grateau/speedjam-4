using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FloatVariable _gameTime;
        // Start is called before the first frame update
    void Start()
    {
        _gameTime = Resources.Load<FloatVariable>("ScriptableObjects/GameTime");
        _gameTime.Value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _gameTime.Value += Time.deltaTime;
    }
}

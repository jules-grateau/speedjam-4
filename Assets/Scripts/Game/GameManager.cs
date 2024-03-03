using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _gameTime;
        // Start is called before the first frame update
    void Start()
    {
        _gameTime.Value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _gameTime.Value += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

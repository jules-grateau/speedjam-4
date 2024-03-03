using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
[RequireComponent(typeof(Collider2D))]
public class LoadSceneOnTrigger : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _sceneLoader.LoadScene();
        }
    }
}

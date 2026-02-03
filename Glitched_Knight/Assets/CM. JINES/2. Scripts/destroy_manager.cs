using UnityEngine;
using UnityEngine.SceneManagement;

public class destroy_manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "gameEnd" ||
            SceneManager.GetActiveScene().name == "gameOver")
        {
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

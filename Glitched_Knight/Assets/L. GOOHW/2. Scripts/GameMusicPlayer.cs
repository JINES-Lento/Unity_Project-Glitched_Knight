using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicPlayer : MonoBehaviour
{
    public AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic.loop = true;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "gameOver") 
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
        else
        {
            Debug.Log("컴포넌트 음악 할당 안함");
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

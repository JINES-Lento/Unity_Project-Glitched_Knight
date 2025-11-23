using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic.loop = true;
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

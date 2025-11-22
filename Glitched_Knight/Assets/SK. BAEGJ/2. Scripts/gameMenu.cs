using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class gameMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Skill"); //마을로 돌아가도록
    }

    public void GameOver()
    {
        if(MPUI.gameoverMP == true || HPUI.gameoverHP == true)
        {
            SceneManager.LoadScene("gameOver");
        }
    }

}
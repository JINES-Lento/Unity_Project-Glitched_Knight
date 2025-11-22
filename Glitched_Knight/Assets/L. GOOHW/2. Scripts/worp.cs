using UnityEngine;
using UnityEngine.SceneManagement;
public class worp : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Vector3 moveDir = collision.contacts[0].normal;

        if (collision.gameObject.CompareTag("Player"))
        {
            //이동방향 감지
            if (moveDir == Vector3.right) {GoToNextScene();}
            else if (moveDir == Vector3.left) {GoToFormerScene();}
            else {Debug.Log("예외상황");}
        }
    }

    void GoToNextScene()
    {
        //다음 씬으로 이동
        if (SceneManager.GetActiveScene().name == "Level1") {SceneManager.LoadScene("Level2");}
        else if (SceneManager.GetActiveScene().name == "Level2") {SceneManager.LoadScene("Level3");}
        else if (SceneManager.GetActiveScene().name == "Level3") {SceneManager.LoadScene("Level4");}
        else if (SceneManager.GetActiveScene().name == "Level4") {SceneManager.LoadScene("Level5");}
        else {Debug.Log("예외상황");}
    }

    void GoToFormerScene()
    {
        //이전 씬으로 이동
        if (SceneManager.GetActiveScene().name == "Level2") {SceneManager.LoadScene("Level1");}
        else if (SceneManager.GetActiveScene().name == "Level3") {SceneManager.LoadScene("Level2");}
        else if (SceneManager.GetActiveScene().name == "Level4") {SceneManager.LoadScene("Level3");}
        else if (SceneManager.GetActiveScene().name == "Level5") {SceneManager.LoadScene("Level4");}
        else {Debug.Log("예외상황");}
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MPUI : MonoBehaviour 
{

    public Slider MP;

    void Start()
    {
        GameManager.Instance.currentMP = 0;
        MP.maxValue = GameManager.Instance.maxMP;
        MP.value = GameManager.Instance.currentMP;
        GameManager.Instance.isGameOver = false;

}

    // Update is called once per frame
    void Update()
    {
        MP.value = GameManager.Instance.currentMP;
    }
}

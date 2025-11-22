using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{

    public Slider HP;

    void Start()
    {
        GameManager.Instance.currentHP = GameManager.Instance.maxHP;
        HP.maxValue = GameManager.Instance.maxHP;
        HP.value = GameManager.Instance.currentHP;
        GameManager.Instance.isGameOver = false;

}

    // Update is called once per frame
    void Update()
    {
        HP.value = GameManager.Instance.currentHP;
    }
}

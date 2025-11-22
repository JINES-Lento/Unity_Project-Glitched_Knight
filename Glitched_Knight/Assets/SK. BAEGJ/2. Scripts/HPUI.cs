using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{

    public float maxHP;
    public float currentHP; //작동 테스트용 현재 HP
    public Slider HP;
    public static bool gameoverHP = false;

    void Start()
    {
        currentHP = maxHP;
        HP.maxValue = maxHP;
        HP.value = currentHP; 
        gameoverHP = false;

}

    // Update is called once per frame
    void Update()
    {
        HP.value = currentHP;
        if (currentHP <= 0)
        {
            gameoverHP = true;
        }
    }
}

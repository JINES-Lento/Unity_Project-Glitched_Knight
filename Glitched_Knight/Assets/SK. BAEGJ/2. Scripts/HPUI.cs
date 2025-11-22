using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{

    public float maxHP;
    public float currentHP; //작동 테스트용 현재 HP
    public Slider HP;
    
    void Start()
    {
        currentHP = maxHP;
        HP.maxValue = maxHP;
        HP.value = currentHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        HP.value = currentHP;
    }
}

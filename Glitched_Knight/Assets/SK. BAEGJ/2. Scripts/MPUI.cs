using UnityEngine;
using UnityEngine.UI;

public class MPUI : MonoBehaviour //더미데이터 현재 스킬란에 통합되어있음
{

    public float maxMP;
    public float currentMP; //작동 테스트용 현재 MP
    public Slider MP;

    void Start()
    {
        currentMP = 0;
        MP.maxValue = maxMP;
        MP.value = currentMP;

    }

    // Update is called once per frame
    void Update()
    {
        MP.value = currentMP;
    }
}

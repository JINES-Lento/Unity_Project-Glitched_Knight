using UnityEngine;
using UnityEngine.UI;

public class MPUI : MonoBehaviour 
{

    public float maxMP;
    public static float currentMP;
    public Slider MP;
    public static bool gameoverMP = false;

    void Start()
    {
        currentMP = 0;
        MP.maxValue = maxMP;
        MP.value = currentMP;
        gameoverMP = false;

}

    // Update is called once per frame
    void Update()
    {
        MP.value = currentMP;
        if (currentMP >= maxMP)
        {
            gameoverMP = true;
        }
    }
}

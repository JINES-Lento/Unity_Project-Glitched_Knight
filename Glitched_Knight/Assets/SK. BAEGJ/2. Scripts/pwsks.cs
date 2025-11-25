using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pwsks : MonoBehaviour
{
    public bool isCooldown = false;
    public float coolTimer = 3f;

    CooldownUI pwsk;

    void Start()
    {
        pwsk = GetComponent<CooldownUI>();
        // UI 초기화
        if (pwsk != null)
        {
            
            pwsk.SetMaxCooldown(coolTimer);
            pwsk.SetCurrentCooldown(coolTimer);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isCooldown)
            {
                StartCoroutine(coolTime(coolTimer));
            }
        }
    }

    IEnumerator coolTime(float cool)
    { //쿨타임
        isCooldown = true;

        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;

            if (pwsk != null)
                pwsk.SetCurrentCooldown(cool);
            yield return null;
        }
        isCooldown = false;
    }
}

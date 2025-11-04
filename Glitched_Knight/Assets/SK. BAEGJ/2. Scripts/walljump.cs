using System.Collections;
using UnityEngine;

public class walljump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float abyss = 0;
    public bool isCooldown = false;
    Vector3 moveDir = Vector3.forward;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) moveDir = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.A)) moveDir = Vector3.left;
        if (Input.GetKeyDown(KeyCode.S)) moveDir = Vector3.back;
        if (Input.GetKeyDown(KeyCode.D)) moveDir = Vector3.right; // 방향 저장

        if (Input.GetKeyDown(KeyCode.Q))
        {
            checkTrigger();
            StartCoroutine(coolTime(1f));
        }
    }
    bool checkTrigger()
    { //스킬 발동 조건 확인
        RaycastHit hit;

        if (Physics.Raycast(transform.position, moveDir, out hit, 2f))
        {
            if (hit.collider.CompareTag("Finish") && !isCooldown)
            {
                Debug.Log("스킬발동");
                transform.position = hit.point + moveDir * 2f; //스킬시전
                abyss += 3;
                return true;
            }
        }
        Debug.Log("트리거 없음");
        return false;
    }

    IEnumerator coolTime(float cool)
    { //쿨타임
        isCooldown = true;
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isCooldown = false;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class new_walljump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isCooldown = false;
    public float coolTimer = 3f;
    public float useMP = 3; //잠식증가량

    public CooldownUI pwsk;

    Vector3 moveDir = Vector3.forward;

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.W)) moveDir = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.A)) moveDir = Vector3.left;
        if (Input.GetKeyDown(KeyCode.S)) moveDir = Vector3.back;
        if (Input.GetKeyDown(KeyCode.D)) moveDir = Vector3.right; // 방향 저장

        

        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (!isCooldown)
            {
            StartCoroutine(coolTime(coolTimer));
                RaycastHit hit;
                RaycastHit hit2;
                if (!Physics.Raycast(transform.position + moveDir * 2f + Vector3.down * 1f, Vector3.up, out hit2, 2f) || (!hit2.collider.CompareTag("passWall") && !hit2.collider.CompareTag("realWall")))
                {
                    if (Physics.Raycast(transform.position, moveDir, out hit, 2f)) //스킬 발동 조건 확인
                    {
                        if (hit.collider.CompareTag("passWall"))
                        {
                            Debug.Log("스킬발동");
                            transform.position = hit.point + moveDir * 2f; //스킬시전
                            GameManager.Instance.currentMP += useMP; 
                        }
                    }
                }
            }
        }
    }

    bool checkTrigger()
    { //스킬 발동 조건 확인
        RaycastHit hit;
        RaycastHit hit2;

        if (!Physics.Raycast(transform.position + moveDir * 2f + Vector3.down * 1f, Vector3.up, out hit2, 2f) ||
        (!hit2.collider.CompareTag("passWall") && !hit2.collider.CompareTag("realWall")))
        {
            if (Physics.Raycast(transform.position, moveDir, out hit, 2f))
            {
                if (hit.collider.CompareTag("passWall") && !isCooldown)
                {
                    Debug.Log("스킬발동");
                    Debug.Log(Physics.Raycast(transform.position + moveDir * 2f, moveDir, out hit2, 0.1f));
                    transform.position = hit.point + moveDir * 2f; //스킬시전

                    return true;
                }
            }
        }
        Debug.Log("트리거 없음");
        Debug.Log(Physics.Raycast(transform.position + moveDir * 2f, moveDir, out hit2, 0.1f));
        return false;
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

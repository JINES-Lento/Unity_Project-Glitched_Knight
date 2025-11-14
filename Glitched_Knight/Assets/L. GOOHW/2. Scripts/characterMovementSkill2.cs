//WASD키를 이용한 캐릭처의 움직임, 카메라 이동 변환 스크립트
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class CharacterMovementSkill2 : MonoBehaviour
{
    public float moveSpeed = 5f;        // 캐릭터 이동 속도
    public Transform cameraTransform;   // 메인 카메라
    public Vector3 cameraOffset = new Vector3(0, 5, -5); // 카메라와 캐릭터 사이 거리

    public float abyss = 0;
    public bool isCooldown = false;
    Vector3 moveDir = Vector3.forward;

   private void Update()
    {
        // 입력값 (WASD)
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1f;
            moveDir = Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S)) 
        { 
            moveZ = -1f;
            moveDir = Vector3.back;
        }

        if (Input.GetKey(KeyCode.A)) 
        { 
            moveX = -1f;
            moveDir = Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) 
        { 
            moveX = 1f;
            moveDir = Vector3.right;
        }

        // 이동 벡터 (XZ 평면만 이동)
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        // 실제 이동 적용
        transform.position += move * moveSpeed * Time.deltaTime;


        // 좌우 반전 (회전)
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1); // 좌우 반전
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);  // 원상복구
        }



        // 카메라 위치 갱신
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.LookAt(transform); // 항상 캐릭터 바라보게
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            checkTrigger();
            StartCoroutine(coolTime(1f));
        }

        Debug.DrawRay(transform.position +  moveDir * 2f + Vector3.down * 1f, Vector3.up * 1f, Color.blue, 0.3f);
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
                    abyss += 3;
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
            yield return new WaitForFixedUpdate();
        }
        isCooldown = false;
    }
}

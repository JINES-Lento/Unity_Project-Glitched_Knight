//WASD키를 이용한 캐릭처의 움직임, 카메라 이동 변환 스크립트
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // 캐릭터 이동 속도
    public Transform cameraTransform;   // 메인 카메라
    public Vector3 cameraOffset = new Vector3(0, 10, -10); // 카메라와 캐릭터 사이 거리

    private void Update()
    {
        // 입력값 (WASD)
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

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
    }
}

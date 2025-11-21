//WASD키를 이용한 캐릭처의 움직임, 카메라 이동 변환 스크립트

using UnityEngine;


public class characterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // 캐릭터 이동 속도
    public Transform cameraTransform;   // 메인 카메라
    public Vector3 cameraOffset = new Vector3(0, 10, -10); // 카메라와 캐릭터 사이 거리
    
    private Animator animator; // 애니메이터

    private void Start()
    {
        animator = GetComponent<Animator>(); //애니메이터 가져오기
    }

    private void Update()
    {
        // 입력값 (WASD)
        float moveX = 0f;
        float moveZ = 0f;

        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);

        if (w) moveZ = 1f;
        if (s) moveZ = -1f;
        if (a) moveX = -1f;
        if (d) moveX = 1f;

        // 이동 벡터 (XZ 평면만 이동)
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        // 실제 이동 적용
        transform.position += move * moveSpeed * Time.deltaTime;



        // 1. direction 값 설정
        if (w) animator.SetInteger("direction", 3);
        else if (a) animator.SetInteger("direction", 1);
        else if (s) animator.SetInteger("direction", 0);
        else if (d) animator.SetInteger("direction", 2);

        // 2. is_moving 처리
        
        bool isMoving = (w || a || s || d);
        animator.SetBool("is_moving", isMoving);

        // 3. is_attacking 처리 (M 키)
        bool isAttacking = Input.GetKey(KeyCode.M);
        animator.SetBool("is_attacking", isAttacking);



        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.StartMPCharge();
        }

        /*
        // 좌우 반전 (회전)
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1); // 좌우 반전
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);  // 원상복구
        }

        */

        // 카메라 위치 갱신
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.LookAt(transform); // 항상 캐릭터 바라보게
        }
    }
}

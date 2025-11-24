//WASD키를 이용한 캐릭처의 움직임, 카메라 이동 변환 스크립트

using UnityEngine;

public class real_move : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;        // 캐릭터 이동 속도
    public Transform cameraTransform;   // 메인 카메라
    public Vector3 cameraOffset = new Vector3(0, 10, -10); // 카메라와 캐릭터 사이 거리

    [Header("Audio Clips")]
    public AudioClip moveClip;   // 걷는 소리 (Inspector에서 할당)
    public AudioClip attackClip; // 공격 소리 (Inspector에서 할당)

    [Header("Status")]
    public bool isMoving = false;
    public bool isAttacking = false;


    // 오디오 소스 2개 (하나는 걷기용, 하나는 효과음용)
    private AudioSource walkSource;
    private AudioSource sfxSource;

    // 공격 상태 추적용 (이전 프레임 상태 기억)
    private bool wasAttacking = false;

    private void Start()
    {


        // 걷기 소리용 AudioSource 생성 및 설정
        walkSource = gameObject.AddComponent<AudioSource>();
        walkSource.loop = true;       // 걷는 소리는 반복되어야 함
        walkSource.clip = moveClip;   // 클립 연결
        walkSource.playOnAwake = false;

        // 2D 사운드로 강제 설정 (거리 상관없이 크게 들림)
        walkSource.spatialBlend = 0f;
        walkSource.volume = 1f; // 볼륨 최대

        // 공격용 AudioSource 생성 및 설정
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;       // 공격 소리는 반복 안 함
        sfxSource.playOnAwake = false;

        // 2D 사운드로 강제 설정
        sfxSource.spatialBlend = 0f;
        sfxSource.volume = 1f;
    }

    public void Update()
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



        // 2. is_moving 처리

        isMoving = (w || a || s || d);
        HandleMoveSound(); // 이동 사운드 함수 호출

        // 3. is_attacking 처리 (M 키)
        isAttacking = Input.GetKey(KeyCode.M);
        HandleAttackSound(); // 공격 사운드 함수 호출



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

    // --- 사운드 처리 함수들 ---

    void HandleMoveSound()
    {
        // moveClip이 없으면 실행 안 함 (에러 방지)
        if (moveClip == null)
        {
            Debug.LogError("걷기 소리 파일이 연결되지 않았습니다!"); // 이 로그가 뜨는지 확인
            return;
        }

        if (isMoving)
        {
            // 움직이는 중인데 소리가 안 나고 있다면 -> 재생
            if (!walkSource.isPlaying)
            {
                walkSource.Play();
                Debug.Log("걷는 소리 재생");
            }
        }
        else
        {
            // 멈췄는데 소리가 나고 있다면 -> 정지
            if (walkSource.isPlaying)
            {
                walkSource.Stop();
            }
        }
    }

    void HandleAttackSound()
    {
        // attackClip이 없으면 실행 안 함
        if (attackClip == null) return;

        // 공격 키를 "막 눌렀을 때" (False -> True 되는 순간) 한 번만 재생
        if (isAttacking && !wasAttacking)
        {
            // PlayOneShot은 걷는 소리와 섞여도 끊기지 않고 자연스럽게 나옴
            sfxSource.PlayOneShot(attackClip);
        }

        // 현재 상태 저장
        wasAttacking = isAttacking;
    }
}

//몹의 상태 관리
using UnityEngine;

public class Mob : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP = 50;
    public int damageToPlayer = 10; // 몹이 플레이어에게 줄 데미지

    private GameManager gameManager;
    private CharacterMovement playerMovement;

    public AudioClip damagedClip; //피격 소리
    private AudioSource damagedSource; //픽격 오디오 소스

    // 연속 공격을 위한 타이머 변수
    private float damageInterval = 0.3f; // 0.3초 간격
    private float nextDamageTime = 0f;   // 다음 데미지 가능한 시간

    // 몹이 플레이어를 공격할 때도 너무 빨리 때리지 않게 하기 위한 쿨타임
    private float nextPlayerDamageTime = 0f;

    

    void Start()
    {
        gameManager = GameManager.Instance;

        // CharacterMovement가 있는 플레이어 오브젝트 찾기
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerMovement = player.GetComponent<CharacterMovement>();

        damagedSource = GetComponent<AudioSource>();
        if (damagedSource == null)
        {
            damagedSource = gameObject.AddComponent<AudioSource>();
            damagedSource.playOnAwake = false; // 시작하자마자 소리나지 않게
            damagedSource.spatialBlend = 0f;   // 2D 사운드 (잘 들리게 설정)
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 체크
        if (other.CompareTag("Player"))
        {
            // 플레이어가 공격 중일 때 (몹이 맞음)
            if (playerMovement.isAttacking)
            {
                // 현재 시간이 다음 공격 가능 시간보다 지났는지 확인
                if (Time.time >= nextDamageTime)
                {
                    Debug.Log("▶ 플레이어 공격(연속) → 몹 HP 감소");
                    PlayDamagedSound();
                    TakeDamage(20);

                   // 다음 공격 시간 설정 (현재 시간 + 0.3초)
                    nextDamageTime = Time.time + damageInterval;
                }
            }
            // 플레이어가 공격 중이 아닐 때 (플레이어가 맞음)
            else
            {
                // 플레이어도 너무 순식간에 죽지 않도록 1초에 한 번만 맞게 설정 (안전을 위해 추가)
                if (Time.time >= nextPlayerDamageTime)
                {
                    Debug.Log("▶ 몹이 플레이어 공격 → 플레이어 HP 감소");
                    gameManager.DamagePlayer(damageToPlayer);
                    nextPlayerDamageTime = Time.time + 1.0f; // 1초 쿨타임
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"Mob HP: {currentHP}");

        if (currentHP <= 0)
            Die();
    }

    void PlayDamagedSound()
    {
        Debug.Log("데미지 사운드 진입");
        if (damagedClip == null)
        {
            Debug.Log("no sound.");
            return;
        }

        //damagedSource.PlayOneShot(damagedClip);
        AudioSource.PlayClipAtPoint(damagedClip, transform.position);
        Debug.Log("피격 사운드 재생됨!");
    }

    void Die()
    {
        Debug.Log("Mob died!");
        Destroy(gameObject);
    }
}

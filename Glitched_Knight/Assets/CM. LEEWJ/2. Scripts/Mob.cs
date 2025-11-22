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

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 체크
        if (other.CompareTag("Player"))
        {
            if (playerMovement == null) return;

            if (playerMovement.isAttacking == false)
            {
                // 플레이어가 공격 중이 아니면 플레이어 HP 감소
                Debug.Log("▶ 몹이 플레이어 공격 → 플레이어 HP 감소");

                gameManager.DamagePlayer(damageToPlayer);
            }
            else
            {
                // 플레이어가 공격 중이면 몹 HP 감소
                Debug.Log("▶ 플레이어 공격 → 몹 HP 감소");

                PlayDamagedSound();
                TakeDamage(20);  // 예시로 20 데미지
                
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
        AudioSource.PlayClipAtPoint(damagedClip, transform.position); //몹 사라져도 사운드 재생
        Debug.Log("피격 사운드 재생됨!");
    }

    void Die()
    {
        Debug.Log("Mob died!");
        Destroy(gameObject);
    }
}

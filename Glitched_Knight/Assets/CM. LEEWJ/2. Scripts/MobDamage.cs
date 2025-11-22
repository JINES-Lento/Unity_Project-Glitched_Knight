//몹의 공격 스크립트
using UnityEngine;

public class MobDamage : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DamagePlayer(damageAmount); //게임매니저로 전송
        }
    }
}

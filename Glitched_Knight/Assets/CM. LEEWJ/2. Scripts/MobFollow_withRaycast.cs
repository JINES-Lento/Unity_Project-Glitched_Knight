//몹이 플레이어를 따라오는 스크립트
using UnityEngine;

public class MobFollow_TagCheck : MonoBehaviour
{
    public Transform player;            
    public float detectionRadius = 50f; 
    public float moveSpeed = 5f;        
    public float obstacleCheckDistance = 3f; 
    public float stopDistance = 1.5f;   

    Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRadius || distance <= stopDistance) return;

        Vector3 moveDir = (player.position - transform.position).normalized;
        moveDir.y = 0;

        Vector3 rayOrigin = col.bounds.center;

        // 레이캐스트 충돌 감지
        if (Physics.Raycast(rayOrigin, moveDir, out RaycastHit hit, obstacleCheckDistance))
        {
            // 부딪힌 물체의 태그가 "Player"가 아닐 때만 회피
            if (!hit.transform.CompareTag("Player")) 
            {
                Vector3 avoidDir = Vector3.Cross(moveDir, Vector3.up).normalized;
                moveDir = avoidDir;
                
                Debug.DrawRay(rayOrigin, moveDir * obstacleCheckDistance, Color.red);
            }
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.identity;
    }

    private void OnDrawGizmos()
    {
        if (GetComponent<Collider>())
        {
            Vector3 rayOrigin = GetComponent<Collider>().bounds.center;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(rayOrigin, 0.1f);

            if (player != null)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                dir.y = 0;
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(rayOrigin, dir * obstacleCheckDistance);
            }
        }
    }
}
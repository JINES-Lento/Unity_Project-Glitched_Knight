//플레이어가 일정 범위에 있을때 몹이 플레이어를 따라오는 코드(레이캐스트 이용)
using UnityEngine;

public class MobFollow_withRaycast : MonoBehaviour
{
    public Transform player;            // 추적할 플레이어
    public float detectionRadius = 50f; // 추격 감지 거리
    public float moveSpeed = 5f;        // 이동 속도
    public float rotationSpeed = 8f;    // 회전 속도
    public float obstacleCheckDistance = 3f; // 장애물 감지 거리

    void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 플레이어가 감지 반경 밖이면 추적하지 않음
        if (distance > detectionRadius) return;

        // 플레이어 방향 계산
        Vector3 targetDir = (player.position - transform.position).normalized;
        targetDir.y = 0;

        // 레이캐스트로 장애물 감지
        if (Physics.Raycast(transform.position + Vector3.up * 1f, transform.forward, out RaycastHit hit, obstacleCheckDistance))
        {
            // 장애물 회피
            // 오른쪽으로 회피 시도
            Vector3 rightDir = transform.right;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rightDir), rotationSpeed * Time.deltaTime);
        }
        else
        {
            // 장애물이 없을 때 플레이어 방향으로 회전
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // 앞으로 이동
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // Scene 뷰에서 시각화
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * obstacleCheckDistance);
    }
}

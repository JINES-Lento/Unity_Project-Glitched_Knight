//카메라 이동
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // 따라갈 대상 (Character)
   public Vector3 offset /*= new Vector3(0, 10, -10)*/; // 카메라 오프셋

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
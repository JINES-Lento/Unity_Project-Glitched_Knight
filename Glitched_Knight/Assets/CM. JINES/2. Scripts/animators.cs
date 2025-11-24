//WASD키를 이용한 캐릭처의 움직임, 카메라 이동 변환 스크립트

using UnityEngine;

public class animators : MonoBehaviour
{
    [Header("Status")]
    public bool isMoving = false;
    public bool isAttacking = false;

    private Animator animator; // 애니메이터

    private void Start()
    {
        animator = GetComponent<Animator>(); //애니메이터 가져오기

    }

    public void Update()
    {
        // 입력값 (WASD)

        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);



        // 1. direction 값 설정
        if (w) animator.SetInteger("direction", 3);
        else if (a) animator.SetInteger("direction", 1);
        else if (s) animator.SetInteger("direction", 0);
        else if (d) animator.SetInteger("direction", 2);


        isMoving = (w || a || s || d);
        animator.SetBool("is_moving", isMoving);

        // 3. is_attacking 처리 (M 키)
        isAttacking = Input.GetKey(KeyCode.M);
        animator.SetBool("is_attacking", isAttacking);

    }

  

  
}

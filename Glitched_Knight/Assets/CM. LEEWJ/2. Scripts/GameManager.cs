//게임메니저
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxHP = 100;
    public int currentHP;

    public int maxMP = 100;
    public float currentMP = 0;
    public bool isChargingMP = false;

    public bool isGameOver = false;

    public float mpChargeSpeed = 20f; // 1초당 증가량

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentHP = maxHP;
        currentMP = 0;
        Debug.Log("게임매니저 스타트");
    }

    void Update()
    {
        if (isGameOver) return;

        // MP 잠식 중이면 증가

        if (currentMP >= maxMP)
        {
            currentMP = 0;
            GameOver();
        }


        if (SceneManager.GetActiveScene().name == "gameEnd")
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Level4" ||
            SceneManager.GetActiveScene().name == "Level4-2")
        {
            currentMP = 0;
        }
    }

    // HP 감소 함수
    public void DamagePlayer(int damage)
    {
        if (isGameOver) return;

        currentHP -= damage;

        Debug.Log($"[GameManager] 현재 HP: {currentHP} (변경된 HP: -{damage})");

        if (currentHP <= 0)
        {
            currentHP = 0;
            GameOver();
        }
    }

    // MP 잠식 시작
    public void StartMPCharge()
    {
        if (!isChargingMP)
        {
            Debug.Log("잠식중");
            currentMP = 0;  // 0부터 재시작
            isChargingMP = true;
        }
    }

    // 게임오버 처리
    void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER!");

        SceneManager.LoadScene("gameOver");
    }
}

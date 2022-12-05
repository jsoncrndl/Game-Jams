using System.Collections;
using UnityEngine;

public class ShooterGame : MonoBehaviour
{
    private DifficultyLevel difficulty;
    private int level;
    public int roundEnemiesDefeated { get; private set; }
    public int splitLevel { get; set; }
    public Camera cam;
    [SerializeField] private EnemySpawner spawner;

    [SerializeField] private int enemiesToLevelUp = 5;
    [SerializeField] private Shooter shooter;

    private float attackTimer;

    bool hit;

    private void Start()
    {
        difficulty = GameManager.instance.levels[0];
        level = 0;
        attackTimer = Random.Range(difficulty.spawnTimer.x, difficulty.spawnTimer.y);
    }

    private void Update()
    {
        if (hit)
        {
            hit = false;
            if (splitLevel < 2)
            {
                spawner.DestroyAll();
                GameManager.instance.SplitGame(this);
            }
            else if (splitLevel == 2)
            {
                Destroy(shooter.gameObject);
                GameManager.instance.Damage();
                spawner.enabled = false;
                enabled = false;
            }
        }

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            attackTimer = Random.Range(difficulty.spawnTimer.x, difficulty.spawnTimer.y);
            spawner.Spawn(1, shooter.transform.position, Random.Range(difficulty.moveSpeed.x, difficulty.moveSpeed.y));
        }
    }

    void IncreaseLevel()
    {
        if (level == 9) return;
        level++;
        difficulty = GameManager.instance.levels[level];
        roundEnemiesDefeated = 0;
    }

    public void EnemyDestroyed()
    {
        roundEnemiesDefeated++;
        if (roundEnemiesDefeated % enemiesToLevelUp == 0)
        {
            IncreaseLevel();
        }

        GameManager.instance.AddScore(difficulty.enemyScore);
    }

    public void OnHit()
    {
        hit = true;
    }
}
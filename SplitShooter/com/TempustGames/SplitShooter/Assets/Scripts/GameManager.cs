using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DifficultyLevel[] levels;
    public int score { get; private set; }
    public int bestSplit;

    private int lives;
    
    public ShooterGame gamePrefab;
    [SerializeField] private float gameSpawnOffset;
    private float curXOffset;

    public UnityEvent onGameEnd;
    public TMPro.TextMeshProUGUI scoreText;

    private AudioSource source;
    public AudioClip shipHit;
    public AudioClip enemyHit;
    public AudioClip shoot;
    public AudioClip levelUp;

    [SerializeField] private float hitStopTimeScale;
    [SerializeField] private float hitStopTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lives = 4;
        source = GetComponent<AudioSource>();
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    private void PlayHitSound()
    {
        source.PlayOneShot(shipHit);
    }

    public void OnHit(ShooterGame game)
    {
        PlayHitSound();
        StartCoroutine(HitStop(game, true));
    }

    private IEnumerator HitStop(ShooterGame game, bool shouldSplit)
    {
        Time.timeScale = hitStopTimeScale;
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = 1;

        if (shouldSplit)
        {
            SplitGame(game);
        }
        else
        {
            game.Destroyed();
        }
    }

    public void EndScreen(ShooterGame game)
    {
        PlayHitSound();
        StartCoroutine(HitStop(game, false));
    }

    private void SplitGame(ShooterGame game)
    {
        bestSplit = Mathf.Max(bestSplit, game.roundEnemiesDefeated);

        ShooterGame game1 = Instantiate(gamePrefab, curXOffset * Vector2.right, Quaternion.identity); // Need to give it a new position
        curXOffset += gameSpawnOffset;
        game1.cam.rect = game.cam.rect;

        ShooterGame game2 = Instantiate(game, curXOffset * Vector2.right, Quaternion.identity);
        curXOffset += gameSpawnOffset;
        game2.cam.rect = game.cam.rect;

        switch (game.splitLevel)
        {
            case 0:
                Split1(game1, game2);
                break;
            case 1:
                Split2(game1, game2);
                break;
            //case 2:
            //    Split3(game1, game2);
            //    break;
        }

        Destroy(game.gameObject);
    }

    public void Damage()
    {
        lives--;
        if (lives == 0)
        {
            EndGame();
        }
    }

    private void Split1(ShooterGame game1, ShooterGame game2)
    {
        game1.splitLevel = 1;
        game2.splitLevel = 1;

        game1.cam.rect = new Rect(game1.cam.rect.x + game1.cam.rect.width / 2, game1.cam.rect.y, game1.cam.rect.width, game1.cam.rect.height);
        game2.cam.rect = new Rect(game2.cam.rect.x - game2.cam.rect.width / 2, game2.cam.rect.y, game2.cam.rect.width, game2.cam.rect.height);
    }

    private void Split2(ShooterGame game1, ShooterGame game2)
    {
        game1.splitLevel = 2;
        game2.splitLevel = 2;

        game1.cam.rect = new Rect(game1.cam.rect.x, game1.cam.rect.y, game1.cam.rect.width, game1.cam.rect.height / 2);
        game2.cam.rect = new Rect(game2.cam.rect.x, game2.cam.rect.y + .5f, game2.cam.rect.width, game2.cam.rect.height / 2);
    }

    //private void Split3(ShooterGame game1, ShooterGame game2)
    //{
    //    game1.splitLevel = 3;
    //    game2.splitLevel = 3;

    //    game1.cam.rect = new Rect(game1.cam.rect.x + game1.cam.rect.width / 2, game1.cam.rect.y, game1.cam.rect.width / 2, game1.cam.rect.height);
    //    game2.cam.rect = new Rect(game2.cam.rect.x - game2.cam.rect.width / 2, game2.cam.rect.y, game2.cam.rect.width / 2, game2.cam.rect.height);
    //}

    public void EndGame()
    {
        scoreText.text = "Score: " + score;
        onGameEnd?.Invoke();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
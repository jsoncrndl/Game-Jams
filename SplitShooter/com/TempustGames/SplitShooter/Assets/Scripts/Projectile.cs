using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float fireSpeed;
    public bool isEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * fireSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.isEnemy)
        {
            //Play sound
            ShooterGame game = GetComponentInParent<ShooterGame>();
            if (game != null)
            {
                game.EnemyDestroyed();
            }
        }

        Shooter shooter = collision.gameObject.GetComponent<Shooter>();

        if (shooter != null)
        {
            shooter.OnHit();
        }

        DestroyProjectile(false);
    }

    public void DestroyProjectile(bool playSound)
    {
        Destroy(gameObject);
    }
}
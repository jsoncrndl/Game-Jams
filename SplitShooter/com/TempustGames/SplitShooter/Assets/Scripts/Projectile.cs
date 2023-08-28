using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float fireSpeed;
    public bool isEnemy;

    public Shooter owner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * fireSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile hitProjectile = collision.gameObject.GetComponent<Projectile>();
        Shooter shooter = collision.gameObject.GetComponent<Shooter>();
        if (hitProjectile != null && hitProjectile.isEnemy)
        {
            if (owner != null)
            {
                owner.game.EnemyDestroyed();
            }
        }
        else if (shooter != null)
        {
            shooter.OnHit();
            //Ship was hit
        }
        else
        {
            //Hit wall
        }

        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Vector2 input;
    private Rigidbody2D rb;

    [SerializeField] private float baseSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileOffset;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float turnSpeed;
    private float curCooldown;
    public ShooterGame game;

    private AudioSource source;
    private Vector2 lastDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1);

        curCooldown = Mathf.Clamp(curCooldown - Time.deltaTime, 0, shootCooldown);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, lastDirection), turnSpeed * Time.deltaTime);
        if (curCooldown == 0 && Input.GetButton("Fire"))
        {
            Shoot();
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = input * baseSpeed;
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        
        if (rb.velocity.sqrMagnitude > 0.01)
        {
            lastDirection = rb.velocity;
        }
    }

    private void Shoot()
    {
        source.PlayOneShot(GameManager.instance.shoot);
        curCooldown = shootCooldown;
        Projectile projectile = Instantiate(projectilePrefab, transform.position + transform.up * projectileOffset, transform.rotation, game.transform);
        projectile.owner = this;
    }

    public void OnHit()
    {
        game.OnHit();
    }
}
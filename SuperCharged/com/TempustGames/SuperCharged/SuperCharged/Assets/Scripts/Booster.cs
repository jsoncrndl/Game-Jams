using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private float boostTime = .7f;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Charger charger = collision.GetComponent<Charger>();
        if (charger != null)
        {
            charger.Boost(boostTime);
            source.Play();
        }
    }
}
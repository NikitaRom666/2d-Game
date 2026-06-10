using UnityEngine;
using UnityEngine.InputSystem;

public class CarSoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource driveSource;
    [SerializeField] private AudioSource brakeSource;
    [SerializeField] private AudioSource crashSource;

    private bool crashed = false;

    private void Start()
    {
        if (driveSource != null)
        {
            driveSource.loop = true;
            driveSource.Play();
        }

        if (brakeSource != null)
        {
            brakeSource.loop = true;
            brakeSource.Stop();
        }

        if (crashSource != null)
        {
            crashSource.loop = false;
            crashSource.Stop();
        }
    }

    private void Update()
    {
        if (crashed) return;

        bool isBraking = Keyboard.current != null && Keyboard.current.spaceKey.isPressed;

        if (isBraking)
        {
            if (brakeSource != null && !brakeSource.isPlaying)
            {
                brakeSource.Play();
            }
        }
        else
        {
            if (brakeSource != null && brakeSource.isPlaying)
            {
                brakeSource.Stop();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryPlayCrash(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryPlayCrash(other.gameObject);
    }

    private void TryPlayCrash(GameObject other)
    {
        if (crashed) return;

        if (!other.CompareTag("EnemyCar")) return;

        crashed = true;

        if (driveSource != null)
        {
            driveSource.Stop();
        }

        if (brakeSource != null)
        {
            brakeSource.Stop();
        }

        if (crashSource != null)
        {
            crashSource.Play();
        }
    }
}
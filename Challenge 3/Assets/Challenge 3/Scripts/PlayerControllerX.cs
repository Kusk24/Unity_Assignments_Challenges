using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip jumpSound;
    private bool isLowEnough;
    public float topBound = 15f; // Set the ceiling height limit
    private bool wasSpacePressed = false; // Track if space was pressed in previous frame


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is below the ceiling/top boundary
        if (transform.position.y < topBound)
        {
            isLowEnough = true;
        }
        else
        {
            isLowEnough = false;
            transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
        }
        // Check if space is currently pressed
        bool isSpacePressed = Input.GetKey(KeyCode.Space);
        
        // While space is pressed and player is low enough, float up
        if (isSpacePressed && !gameOver && isLowEnough)
        {
            // Only play jump sound when space is first pressed (not held)
            if (!wasSpacePressed)
            {
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
            if (!(transform.position.y == topBound))
            {
                playerRb.AddForce(Vector3.up * floatForce);
            }
        }
        
        // Update the previous frame's space key state
        wasSpacePressed = isSpacePressed;
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce * 10);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

    }

}

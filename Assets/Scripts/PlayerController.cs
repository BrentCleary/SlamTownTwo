using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player RigidBody
    private Rigidbody playerRB;
    public float jumpForce = 20;
    public float gravityModifier;
    
    
    // Player Animator
    private Animator playerAnim;
    // Player AudioSource
    private AudioSource playerAudio;
    
    
    public bool isOnGround = true;
    public bool gameOver;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        JumpScript();
        // PlayerBoundary();
    }

    private void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if(other.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            
            playerAudio.PlayOneShot(crashSound, 1.0f);

            // Particle Animations
            explosionParticle.Play();
            dirtParticle.Stop();
        }

    }

    // Jump Script - Activates when player presses SpaceBar
    // Set to run in Update Method
    // Controls
    //  - Jump Audio Sounds
    //  - Dirt Particle
    //  - onGround Bool
    public void JumpScript()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
        }
    }

        // if Player Transform Z position is greater/less than boundaries, set position to boundaries
    public void PlayerBoundary()
    {
        // LeftBoundary
        if(transform.position.z >= 10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 10);
        }
        // RightBoundary
        if(transform.position.z <= -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player RigidBody
    public Rigidbody playerRb;
    public float jumpForce = 700f;
    public float gravityModifier = 20f;
    private float animalCollision = 2000f;

    // Player Animator
    private Animator playerAnimator;
    
    // Player AudioSource
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Triggers
    public bool gameOver;
    public bool mooseCollision = false;
    public bool isOnGround = true;
    public bool secondJump;
    private bool coolTime;
    public float coolTimeLength = 0.2f;

    // Particle System
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem jumpParticle;
    public Vector3 jumpParticleSpawnPos;

    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        // JumpScript();
        Jump1();
        Jump2();
    }

    public void Jump1()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Movement
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Triggers
            isOnGround = false;
            secondJump = true;
            coolTime = true;
            Invoke("CoolDown", coolTimeLength);

            // Animations
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            jumpParticleSpawnPos = transform.position;

            Instantiate(jumpParticle, jumpParticleSpawnPos, jumpParticle.transform.rotation);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            
        }
    }

    public void Jump2()
    {
        if(Input.GetKeyDown(KeyCode.Space) && secondJump && !coolTime && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump_trig");
            secondJump = false;

            jumpParticleSpawnPos = transform.position;
            Instantiate(jumpParticle, jumpParticleSpawnPos, jumpParticle.transform.rotation);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Ground
        if(other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        // Obstacle
        if(other.gameObject.CompareTag("Obstacle"))
        {
            // Animations
            playerRb.AddForce(Vector3.left * animalCollision, ForceMode.Impulse);
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

            // Particles
            explosionParticle.Play();
            dirtParticle.Stop();
        
            // Sound
            playerAudio.PlayOneShot(crashSound, 1.0f);
            
            // Triggers
            gameOver = true;
            Debug.Log("Game Over");
        }

        // Animal
        if (other.gameObject.CompareTag("Animal"))
        {
            // Triggers
            gameOver = true;
            mooseCollision = true;
            Debug.Log("Hit Animal");

            // Animations
            playerRb.AddForce(Vector3.left * animalCollision, ForceMode.Impulse);
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

            // Particles
            explosionParticle.Play();
            dirtParticle.Stop();

            // Sound
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }

    }

    void CoolDown()
    {
        coolTime = false;
    }
    
}
    

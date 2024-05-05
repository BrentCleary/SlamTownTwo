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
    public bool boostOn;
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
    private float jumpParticleOffset_X_ = 1.5f;
    private float jumpParticleOffset_Y_ = 1f;

    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        
    }


    // Update is called once per frame
    void Update()
    {

        Jump1();
        Jump2();
        Boost();
        
    }

    public void Jump1()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Movement
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Triggers
            boostOn = true;
            isOnGround = false;
            secondJump = true;
            coolTime = true;
            Invoke("CoolDown", coolTimeLength);

            // Animations
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();

            Instantiate(jumpParticle, JumpParticlePostion(), jumpParticle.transform.rotation);
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

            Instantiate(jumpParticle, JumpParticlePostion(), jumpParticle.transform.rotation);
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
        if(other.gameObject.CompareTag("Animal"))
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
            mooseCollision = true;
            gameOver = true;
            Debug.Log("Hit Animal");
        }

    }

    void CoolDown()
    {
        coolTime = false;
    }
    
    void Boost()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            boostOn = true;
        }
        else
        {
            boostOn = false;
        }
    }

    public Vector3 JumpParticlePostion()
    {
        // Spawn of jump explosions
        return jumpParticleSpawnPos = new Vector3(playerRb.transform.position.x + jumpParticleOffset_X_, 
                                                  playerRb.transform.position.y + jumpParticleOffset_Y_,
                                                  playerRb.transform.position.z);
    }

}
    

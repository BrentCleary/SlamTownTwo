using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player RigidBody
    public Rigidbody playerRb;
    public float jumpForce = 20f;

    // Collision Properties
    private float collisionForceTotal;
    private float collisionForceBasic = 2000f;
    private float collisionSpeedModifier; // Based on gameManager Speed
    private float collisionSpeedReducer = .01f;

    // Player Animator
    private Animator playerAnimator;
    
    // Player AudioSource
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Speed Triggers
    public bool boostOn; // Triggers Speed increase of 20 in GameManagerScript
    public bool speedTestOn; // Trigger Speed increase of 2000 in GameManagerScript

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

    public GameManager gameManagerScript;

    public float gravityModifier = 30f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier;

    }


    // Update is called once per frame
    void Update()
    {

        // Gameplay Functions
        Jump1();
        Jump2();
        Boost();

        // Debug Functions
        TestSpeedFunction();
        SpeedTestBoost();

        collisionSpeedModifier = gameManagerScript.gameSpeed;
        collisionForceTotal = collisionForceBasic * collisionSpeedModifier * collisionSpeedReducer;

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

    public void TestSpeedFunction()
    {
        if(Input.GetKeyDown(KeyCode.R) && isOnGround && !gameOver)
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
            playerRb.AddForce(Vector3.left * collisionForceTotal, ForceMode.Impulse);
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
            playerRb.AddForce(Vector3.left * collisionForceTotal, ForceMode.Impulse);
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

    void SpeedTestBoost()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            speedTestOn = true;
        }
        else
        {
            speedTestOn = false;
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
    

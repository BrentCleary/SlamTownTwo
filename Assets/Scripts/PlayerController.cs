using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player RigidBody
    public Rigidbody playerRb;
    public float jumpForce = 50f;
    public float gravityModifier = 20f;

    // Player Animator
    private Animator playerAnim;
    
    // Player AudioSource
    private AudioSource playerAudio;
    
    public bool gameOver;

    public bool isOnGround = true;
    public bool secondJump;
    private bool coolTime;
    public float coolTimeLength = 0.2f;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem jumpParticle;
    public Vector3 jumpParticleSpawnPos;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
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
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) //gameOver != true
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            secondJump = true;
            coolTime = true;
            Invoke("CoolDown", coolTimeLength);


            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            
            jumpParticleSpawnPos = transform.position;
            Instantiate(jumpParticle, jumpParticleSpawnPos, jumpParticle.transform.rotation);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    public void Jump2()
    {
        if(Input.GetKeyDown(KeyCode.Space) && secondJump && !coolTime && !gameOver) //gameOver != true
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            secondJump = false;

            jumpParticleSpawnPos = transform.position;
            Instantiate(jumpParticle, jumpParticleSpawnPos, jumpParticle.transform.rotation);
            playerAudio.PlayOneShot(jumpSound, 1.0f);

            
        }
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

    void CoolDown()
    {
        coolTime = false;
    }
    
}
    

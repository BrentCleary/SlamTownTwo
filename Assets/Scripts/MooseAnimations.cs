using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseAnimations : MonoBehaviour
{

    private Animator mooseAnimator;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        mooseAnimator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.mooseCollision == true)
        {
            MooseEatAnimation();
        }
    }

    void MooseEatAnimation()
    {
        mooseAnimator.SetFloat("Speed_f", 0f);
        mooseAnimator.SetBool("Eat_b", true);
    }
}

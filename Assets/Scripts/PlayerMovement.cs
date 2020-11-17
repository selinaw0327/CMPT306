using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private Vector3 movement;

    // Sound effect
    private AudioSource[] sounds;
    private AudioSource footstep;
    private AudioSource attack;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        // auto-get components at runtime
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        // Sound effects
        sounds = GetComponents<AudioSource>();

        footstep = sounds[0];
        attack = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.33f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (movement != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void MoveCharacter()
    {
        // moves the character
        transform.Translate(new Vector3(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime));
    }

    public void Footstep()
    {
        footstep.Play();
    }

    public void Attack()
    {
        Debug.Log("ATTACK");
        attack.Play();
    }
}

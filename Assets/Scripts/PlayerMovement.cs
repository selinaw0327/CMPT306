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
    private Vector2 attackDirection;

    // Sound effect
    private AudioSource[] sounds;
    private AudioSource footstep;
    private AudioSource attack;

    // Equipped sword
    public bool swordEquipped;

    // slope of screen window for click calculation
    private readonly float screenSlope = (float)Screen.height / Screen.width;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        // auto-get components at runtime
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        animator.SetFloat("attackX", 0);
        animator.SetFloat("attackY", -1);

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

        attackDirection = Vector2.zero;
        //if (Input.mousePosition.x > (Screen.width/2))
        //{
        //    attackDirection.x = 1;
        //    attackDirection.y = 0;
        //}
        //else
        //{
        //    attackDirection.x = -1;
        //    attackDirection.y = 0;
        //}


        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            float direction = GetClickDirection(Input.mousePosition.x, Input.mousePosition.y);
            switch (direction)
            {
                case 0:
                    attackDirection.x = 0;
                    attackDirection.y = 1;
                    break;
                case 1:
                    attackDirection.x = 1;
                    attackDirection.y = 0;
                    break;
                case 2:
                    attackDirection.x = 0;
                    attackDirection.y = -1;
                    break;
                case 3:
                    attackDirection.x = -1;
                    attackDirection.y = 0;
                    break;
            }
            if (swordEquipped)
            {
                StartCoroutine(AttackCo());
            }
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
        animator.SetFloat("attackX", attackDirection.x);
        animator.SetFloat("attackY", attackDirection.y);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.1f);
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

    public float GetClickDirection(float x, float y)
    {
        // calculates the direction of the mouse click relative to the player
        // returns:
        //      0 if above player
        //      1 if to the right of the player
        //      2 if below the player
        //      3 if to the left of the player
        float first_diagonal_y = x * screenSlope;
        float second_diagonal_y = x * (-screenSlope) + Screen.height;
        if (first_diagonal_y >= y)
        {
            if (second_diagonal_y >= y)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (second_diagonal_y >= y)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }
    
    public void Footstep()
    {
        footstep.Play();
    }

    public void Attack()
    {
        attack.Play();
    }
}

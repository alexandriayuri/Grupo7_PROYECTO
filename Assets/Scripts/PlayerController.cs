using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    //private bool isFacingRight;

    // collisions
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f; // keeps player from getting stuck 
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            animator.SetBool("IsMoving", true);
        }

        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(movement * speed);
        //rb.MovePosition(rb.position +  (movement * speed * Time.fixedDeltaTime));

        bool success = MovePlayer(movement);
    }

    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            direction, // x and y values beween -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // the settings that determine where a collision can occur on such as layers to collide
            castCollisions, // list of collisions to store the found collisions into after the Cast is finished
            speed * Time.fixedDeltaTime + collisionOffset); // the amount to cast equeal to the movement plus offset of the collisions

        if (count == 0)
        {
            Vector2 moveVector = direction * speed * Time.fixedDeltaTime;

            //No collisions 
            rb.MovePosition(rb.position + moveVector);
            return true;
        }

        else
        {
            // Print collisions: debug to see what were the hits/what the character's colliding with
            foreach (RaycastHit2D hit in castCollisions)
            {
                print(hit.ToString());
            }
            return false;
        }
    }
}

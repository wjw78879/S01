using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb2d;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool started = false;

    bool stopped = false;

    bool onCD = false;

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnAiring()
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnCrouching(bool crouch)
    {
        if (!crouch)
            animator.SetBool("IsCrouching", false);
    }

    public void Starting()
    {
        animator.SetBool("started", true);
        started = true;
    }

    public void RestartGame()
    {
        crouch = false;
        horizontalMove = 0f;
        animator.SetBool("started", false);
        if (animator.GetBool("IsCrouching"))
            animator.SetBool("IsCrouching", false);
        animator.SetFloat("Speed", 0f);
        animator.SetFloat("VerticalSpeed", 0f);
        started = false;
    }

    public void SetActive(bool torf)
    {
        if (torf == true)
        {
            stopped = false;
        }
        else
        {
            stopped = true;
            crouch = false;
            horizontalMove = 0f;
            if (animator.GetBool("IsCrouching"))
                animator.SetBool("IsCrouching", false);
        }
    }

    public void Switching(string direction)
    {
        stopped = true;
        crouch = false;
        if (direction == "l")
        {
            horizontalMove = -runSpeed;
        }
        else if (direction == "r")
        {
            horizontalMove = runSpeed;
        }
        if (animator.GetBool("IsCrouching"))
            animator.SetBool("IsCrouching", false);
    }

    public bool Stopped()
    {
        return stopped;
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalSpeed", rb2d.velocity.y);
        if (started && !stopped)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;


            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
            if (Input.GetButton("Crouch"))
            {
                crouch = true;
                if (!animator.GetBool("IsCrouching"))
                    animator.SetBool("IsCrouching", true);
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
            if (!onCD && Input.GetButtonDown("Fire1"))
            {
                controller.Shoot();
                onCD = true;
                Invoke("RecoverCD", 0.5f);
            }
        }
        
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void RecoverCD()
    {
        onCD = false;
    }
}

using UnityEngine;

public class move : MonoBehaviour
{
    public LineHelp player;

    public playerControle controller;
    public Animator animator;

    public float runSpeed = 40f;


    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public static move instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plusieurs move dans la scene");
            return;
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

                horizontalMove = 0f;

                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

                animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetTrigger("Jump");
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("Crouch", crouch);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("Crouch", crouch);
        }
    }


    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, ref jump);
    }
}
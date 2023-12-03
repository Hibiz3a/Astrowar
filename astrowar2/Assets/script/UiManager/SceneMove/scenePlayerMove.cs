using System.Collections;
using UnityEngine;

public class scenePlayerMove : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public float delay;
    public bool down;
    public bool left;
    public bool right;
    public int speed;
    public bool up;
    private Vector3 destination;
    Rigidbody2D rb;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            Debug.Log("collision");
            speed = -speed;
        }

    }

    private void move()
    {
        Vector3 direction = (destination - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    IEnumerator SetDestination()
    {
        while (true)
        {
            SetDirection();
            yield return new WaitForSeconds(delay);
        }
    }

    void SetDirection()
    {
        int direction = Random.Range(1, 4);
        switch (direction)
        {
            case 1:
                destination += Vector3.right * Random.Range(1, 10);
                right = true;
                left = false;
                down = false;
                up = false;
                animator.SetBool("down", down);
                animator.SetBool("up", up);
                animator.SetBool("left", left);
                animator.SetBool("right", right);
                break;

            case 2:
                destination += Vector3.left * Random.Range(1, 10);
                left = true;
                right = false;
                down = false;
                up = false;
                animator.SetBool("down", down);
                animator.SetBool("up", up);
                animator.SetBool("left", left);
                animator.SetBool("right", right);
                break;

            case 3:
                destination += Vector3.up * Random.Range(1, 10);
                up = true;
                right = false;
                down = false;
                left = false;
                animator.SetBool("down", down);
                animator.SetBool("up", up);
                animator.SetBool("left", left);
                animator.SetBool("right", right);
                break;

            case 4:
                destination += Vector3.down * Random.Range(1, 10);
                down = true;
                right = false;
                left = false;
                up = false;
                animator.SetBool("down", down);
                animator.SetBool("up", up);
                animator.SetBool("left", left);
                animator.SetBool("right", right);
                break;
        }
    }

    private void Start()
    {
        StartCoroutine(SetDestination());
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        move();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    //this is for movement var
    [Header("Movement Variables")]
    //run var
    public float speed;
    private float MoveX;
    //jump var
    public float JumpForce;
    public LayerMask Ground;
    public float rayJumpDistance;
    public Vector2 rayJumpPos;
    private Color rayColor;
    private bool jump;
    private float inAir;
    //essential var
    private Animator anim;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;

    [Header("Camera Move Effect")]
    public Animator mainCameraAnim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //this for run and idle
        MoveX = Input.GetAxis("Horizontal");
        transform.Translate(MoveX * speed * Time.deltaTime, 0, 0);
        if(Mathf.Abs(MoveX) > 0)
        {
            anim.SetBool("Run", true);
            mainCameraAnim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
            mainCameraAnim.SetBool("Run", false);
        }

        //this for jump and fall
        RaycastHit2D rayJump_one = Physics2D.Raycast(new Vector2(col.bounds.center.x + rayJumpPos.x, col.bounds.center.y), Vector2.down, rayJumpDistance, Ground);
        Debug.DrawRay(new Vector2(col.bounds.center.x + rayJumpPos.x, col.bounds.center.y), Vector2.down * rayJumpDistance, rayColor);
        RaycastHit2D rayJump_two = Physics2D.Raycast(new Vector2(col.bounds.center.x - rayJumpPos.x, col.bounds.center.y), Vector2.down, rayJumpDistance, Ground);
        Debug.DrawRay(new Vector2(col.bounds.center.x - rayJumpPos.x, col.bounds.center.y), Vector2.down * rayJumpDistance, rayColor);
        if(rayJump_one.collider || rayJump_two.collider)
        {
            jump = true;
            anim.SetBool("Fall", false);
            if (inAir > 0)
            {
                anim.SetBool("Jump", false);
                inAir = 0;
            }
        }
        else
        {
            jump = false;
            inAir += Time.deltaTime;
            if(rb.velocity.y < 0)
            {
                anim.SetBool("Fall", true);
                anim.SetBool("Jump", false);
            }
        }
        if (rayJump_one.collider)
        {
            rayColor = Color.red;
        }
        else
        {
            rayColor = Color.green;
        }
        if (rayJump_two.collider)
        {
            rayColor = Color.red;
        }
        else
        {
            rayColor = Color.green;
        }
        if (jump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                anim.SetBool("Jump", true);
            }
        }

        //this will be deleted when the turn animation arrive
        if (MoveX > 0)
            transform.localScale = new Vector2(1, 1);
        if (MoveX < 0)
            transform.localScale = new Vector2(-1, 1);
    }
}

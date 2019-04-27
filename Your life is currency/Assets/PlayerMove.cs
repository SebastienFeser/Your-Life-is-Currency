using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private bool facing_right = true;
    private bool jump = false;
    //private bool grounded = false;
    public const int move_force = 5;
    private const int projectile_speed = 10;
    public float jump_velocity = 8f;
    public float fall_multiplier = 9f;
    public float lowjump_multiplier = 2f;
    //public Transform ground_check;
    //public AudioSource flameSound;
    //public AudioSource randomJumpSound;
    //public AudioClip[] audioSources;
    // private Animator anim;
    private Rigidbody2D rigid;
    public GameObject projectile_prefab;
    private Vector2 player_pos;


    void Awake()
    {
        //anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, ground_check.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown("space"))
        {
            jump = true;
        }
        if (Input.GetKeyDown("w"))
        {
            RangeAttack();
        }
    }

    void FixedUpdate()
    {
        
        float h = Input.GetAxisRaw("Horizontal");

        //anim.SetFloat("Speed", Mathf.Abs(h));
        if (h > 0 && !facing_right)
            Flip();
        else if (h < 0 && facing_right)
            Flip();
        h *= move_force;
        rigid.velocity = new Vector2(h, rigid.velocity.y);
        //randomJumpSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        //randomJumpSound.Play();
        //anim.SetTrigger("Jump");
        if (jump)
        {
            rigid.velocity = Vector2.up * jump_velocity;
            jump = false;
        }
        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (fall_multiplier - 1) * Time.fixedDeltaTime;

        }
        else if (rigid.velocity.y > 0 && !Input.GetKey("space"))
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowjump_multiplier - 1) * Time.fixedDeltaTime;

        }
    }

    
    void Flip()
    {
        facing_right = !facing_right;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    void RangeAttack()
    {
        player_pos = transform.position;
        GameObject projectile = Instantiate(projectile_prefab, player_pos, gameObject.transform.rotation);

        if (facing_right)
        {
            projectile.GetComponent<Rigidbody2D>().velocity = (transform.right * projectile_speed);
        }else
            projectile.GetComponent<Rigidbody2D>().velocity = (transform.right * (projectile_speed * -1));
        Destroy(projectile, 1.0f);
    }

}

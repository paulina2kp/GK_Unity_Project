using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public Rigidbody my_Rigidbody;
    public Animator my_Animator;
    public SpriteRenderer my_SpriteRenderer;
    public Transform sprite_Transform;
    public Transform my_Transform;
    public float move_Speed;

    private Vector2 move_Input;
    private Vector3 only_Rotation = new Vector3(0,0,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        move_Input.x = Input.GetAxis("Horizontal");
        move_Input.y = Input.GetAxis("Vertical");

        my_Rigidbody.linearVelocity = new Vector3(move_Input.x * move_Speed, my_Rigidbody.linearVelocity.y, move_Input.y * move_Speed);
        //my_Transform.eulerAngles = new Vector3(0, 0, 0);

        if (move_Input.x == 0 && move_Input.y == 0)
        {
            my_Animator.SetBool("isMoving", false);
        }
        else
        {
            my_Animator.SetBool("isMoving", true);
        }

        if (!my_SpriteRenderer.flipX && move_Input.x < 0)
        {       
            sprite_Transform.localScale = new Vector3(-1, 1, 1);
            my_SpriteRenderer.flipX = true;
        }
        else if (my_SpriteRenderer.flipX && move_Input.x > 0)
        {
            sprite_Transform.localScale = new Vector3(1, 1, 1);
            my_SpriteRenderer.flipX = false;
        }

    }

}

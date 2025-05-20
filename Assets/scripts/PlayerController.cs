using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    public Rigidbody my_Rigidbody;
    public Animator my_Animator;
    public SpriteRenderer my_SpriteRenderer;
    public Transform sprite_Transform;
    public Transform my_Transform;
    public float move_Speed;

    private Vector2 move_Input;

    private float player_health = 90;
    public Image health_bar;
    public TextMeshProUGUI health_number;
    private float player_hunger = 50;
    public Image hunger_bar;
    public TextMeshProUGUI hunger_number;
    private float player_stamina = 80;
    public Image stamina_bar;
    public TextMeshProUGUI stamina_number;


    void Start()
    {

        health_bar.fillAmount = (float)(player_health * 0.01);
        hunger_bar.fillAmount = (float)(player_hunger * 0.01);
        stamina_bar.fillAmount = (float)(player_stamina * 0.01);
        health_number.text = player_health.ToString();
        hunger_number.text = player_hunger.ToString();
        stamina_number.text = player_stamina.ToString();

    }


    void Update()
    {
        move_Input.x = Input.GetAxis("Horizontal");
        move_Input.y = Input.GetAxis("Vertical");

        my_Rigidbody.linearVelocity = new Vector3(move_Input.x * move_Speed, my_Rigidbody.linearVelocity.y, move_Input.y * move_Speed);

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

        HealthLimit();
        HungerLimit();
        StaminaLimit();
    }

    public void HealPlayer(float amount)
    {
        Debug.Log("ilosc " + amount);
        player_health = player_health + amount;
        Debug.Log("health " + player_health);
        health_number.text = player_health.ToString();
        health_bar.fillAmount = (float)(player_health * 0.01);
    }
    public void DamagePlayer(float amount)
    {
        Debug.Log("ilosc " + amount);
        player_health = player_health - amount;
        Debug.Log("health " + player_health);
        health_number.text = player_health.ToString();
        health_bar.fillAmount = (float)(player_health * 0.01);
    }

    public void HealthLimit()
    {
        if (player_health > 100)
        {
            player_health = 100;
            health_number.text = player_health.ToString();
            health_bar.fillAmount = (float)(100 * 0.01);
        }
        else if (player_health < 0)
        {
            player_health = 0;
            health_number.text = player_health.ToString();
            health_bar.fillAmount = (float)(0 * 0.01);
        }
    }

    public void HungerLimit()
    {
        if (player_hunger > 100)
        {
            player_hunger = 100;
            hunger_number.text = player_hunger.ToString();
            hunger_bar.fillAmount = (float)(100 * 0.01);
        }
        else if (player_hunger < 0)
        {
            player_hunger = 0;
            hunger_number.text = player_hunger.ToString();
            hunger_bar.fillAmount = (float)(0 * 0.01);
        }
    }

    public void StaminaLimit()
    {
        if (player_stamina > 100)
        {
            player_stamina = 100;
            stamina_number.text = player_stamina.ToString();
            stamina_bar.fillAmount = (float)(100 * 0.01);
        }
        else if (player_stamina < 0)
        {
            player_stamina = 0;
            stamina_number.text = player_stamina.ToString();
            stamina_bar.fillAmount = (float)(0 * 0.01);
        }
    }
}

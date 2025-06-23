using System.Collections;
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
    public GameObject loot_prefab;
    public GameObject my_player;
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

    public bool isStarving;
    private float hungerTime = 5;
    public int hungerAmount;


    void Start()
    {

        health_bar.fillAmount = (float)(player_health * 0.01);
        hunger_bar.fillAmount = (float)(player_hunger * 0.01);
        stamina_bar.fillAmount = (float)(player_stamina * 0.01);
        health_number.text = player_health.ToString();
        hunger_number.text = player_hunger.ToString();
        stamina_number.text = player_stamina.ToString();

        InvokeRepeating("GettingHungry", hungerTime, hungerTime);
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
    public void PlayerEats(float amount)
    {
        Debug.Log("ilosc " + amount);
        player_hunger = player_hunger + amount;
        Debug.Log("hunger " + player_hunger);
        hunger_number.text = player_hunger.ToString();
        hunger_bar.fillAmount = (float)(player_hunger * 0.01);
    }

    public void GettingHungry()
    {
        if (isStarving && player_hunger > 0) { 
        player_hunger = player_hunger - hungerAmount;
        Debug.Log("G£ÓD SPADA " + player_hunger);
        hunger_number.text = player_hunger.ToString();
        hunger_bar.fillAmount = (float)(player_hunger * 0.01);
        }
        else if(player_hunger <= 0)
        {
            DamagePlayer(1f);
        }
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

    public void DropFromEQ(Loot one_item)
    {
        Debug.Log("jestem w DEQ");
        Vector3 position = transform.position;

        //float random_range = Random.Range(-1.5f, 1.5f);
        Vector3 spawn_position = new Vector3(position.x + Random.Range(-1.5f, 1.5f), position.y, position.z + Random.Range(-1.5f, 1.5f));
        GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);

        spawned_object.GetComponent<PickUp>().my_Player = my_player;
        spawned_object.GetComponent<PickUp>().item = one_item;
        spawn_position = new Vector3(position.x, position.y, position.z);
        spawned_object.GetComponent<SpriteRenderer>().sprite = one_item.loot_sprite;

    }
}

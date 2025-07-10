using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody my_Rigidbody;
    public Animator my_Animator;
    public Animator blood_Animator;
    public SpriteRenderer my_SpriteRenderer;
    public Transform sprite_Transform;
    public Transform my_Transform;
    public GameObject loot_prefab;
    public GameObject my_player;
    public float move_Speed;

    private Vector2 move_Input;

    private float player_health = 100;
    public Image health_bar;
    public TextMeshProUGUI health_number;
    private float player_hunger = 100;
    public Image hunger_bar;
    public TextMeshProUGUI hunger_number;
    private float player_stamina = 100;
    public Image stamina_bar;
    public TextMeshProUGUI stamina_number;

    public bool isStarving;
    private float hungerTime = 5;
    public int hungerAmount;

    private WorldTime worldTime;
    public bool safeSpace = false;
    private bool isDead = false;
    void Start()
    {
        worldTime = FindFirstObjectByType<WorldTime>();

        health_bar.fillAmount = (float)(player_health * 0.01);
        hunger_bar.fillAmount = (float)(player_hunger * 0.01);
        stamina_bar.fillAmount = (float)(player_stamina * 0.01);
        health_number.text = player_health.ToString();
        hunger_number.text = player_hunger.ToString();
        stamina_number.text = player_stamina.ToString();

        InvokeRepeating("GettingHungry", hungerTime, hungerTime);
        InvokeRepeating("PlayerTired", 5, 5);
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

        if(player_health == 0 && !isDead )
        {
            PlayerDie();
        }
    }

    public void HealPlayer(float amount)
    {
        player_health = player_health + amount;
        health_number.text = player_health.ToString();
        health_bar.fillAmount = (float)(player_health * 0.01);
    }
    public void DamagePlayer(float amount)
    {
        player_health = player_health - amount;
        health_number.text = player_health.ToString();
        health_bar.fillAmount = (float)(player_health * 0.01);
    }
    public void PlayerEats(float amount)
    {
        player_hunger = player_hunger + amount;
        hunger_number.text = player_hunger.ToString();
        hunger_bar.fillAmount = (float)(player_hunger * 0.01);
    }

    public void GettingHungry()
    {
        if (isStarving && player_hunger > 0) { 
        player_hunger = player_hunger - hungerAmount;
        hunger_number.text = player_hunger.ToString();
        hunger_bar.fillAmount = (float)(player_hunger * 0.01);
        }
        else if(player_hunger <= 0)
        {
            DamagePlayer(1f);
        }
    }
    public void PlayerTired()
    {
        if (player_stamina <= 0)
        {
            DamagePlayer(1f);
        }
        else if (worldTime.isNight && safeSpace == false)
        {
            player_stamina = player_stamina - 1;        
            stamina_number.text = player_stamina.ToString();
            stamina_bar.fillAmount = (float)(player_stamina * 0.01);
        }
    }

    public void GetStamina(float amount)
    {
        player_stamina = player_stamina + amount;
        stamina_number.text = player_stamina.ToString();
        stamina_bar.fillAmount = (float)(player_stamina * 0.01);
    }

    public void LoseStamina(float amount)
    {
        player_stamina = player_stamina - amount;
        stamina_number.text = player_stamina.ToString();
        stamina_bar.fillAmount = (float)(player_stamina * 0.01);
    }

    public void SpeedPlayer()
    {
        float currSpeed = move_Speed;
        move_Speed = 20f;
        StartCoroutine(Wait10sec(currSpeed));
    }
    private IEnumerator Wait10sec(float speed)
    {
        yield return new WaitForSeconds(10);
        move_Speed = speed;
    }

    private IEnumerator Wait1sec()
    {
        yield return new WaitForSeconds(0.5f);
        var bloodHolder = transform.GetChild(3);
        bloodHolder.gameObject.SetActive(false);
        SceneManager.LoadScene("GameOverScene");
    }

    public void PlayerDie()
    {

        var bloodHolder = transform.GetChild(3);
        bloodHolder.gameObject.SetActive(true);
        blood_Animator.SetTrigger("Die");
        isDead = true;
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(Wait1sec());
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
        Vector3 position = transform.position;
        Vector3 spawn_position = new Vector3(position.x + Random.Range(-1.5f, 1.5f) , position.y, position.z + Random.Range(-1.5f, 1.5f));
        GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);

        spawned_object.GetComponent<PickUp>().my_Player = my_player;
        spawned_object.GetComponent<PickUp>().item = one_item;
        spawned_object.GetComponent<SpriteRenderer>().sprite = one_item.loot_sprite;

    }

    public void PlaceTorch(GameObject prefab)
    {
        Vector3 position = my_player.transform.GetChild(0).position;
        Vector3 spawn_position = new Vector3(position.x, position.y, position.z - 3.5f);
        GameObject spawned_object = Instantiate(prefab, spawn_position, Quaternion.identity);
        spawned_object.GetComponent<TorchFunction>().my_player = my_player;
    }

    public void PlaceMine(GameObject prefab)
    {
        Vector3 position = transform.position;
        Vector3 spawn_position = new Vector3(position.x, position.y, position.z);
        GameObject spawned_object = Instantiate(prefab, spawn_position, Quaternion.identity);
        spawned_object.GetComponent<MineFunction>().my_Player = my_player;
    }

    public void PlaceCauldron(GameObject prefab)
    {
        Vector3 position = transform.position;
        Vector3 spawn_position = new Vector3(position.x, position.y, position.z);
        GameObject spawned_object = Instantiate(prefab, spawn_position, Quaternion.identity);
        spawned_object.GetComponent<CauldronFunction>().my_Player = my_player;
    }

    public void StoreInChest(Loot item)
    {
        ChestInventory[] chests = FindObjectsByType<ChestInventory>(FindObjectsSortMode.None);

        foreach (ChestInventory chest in chests)
        {
            if (chest.playerInRange)
            {
                bool success = chest.Add(item);
                if (success)
                {
                    return; 
                }
                else
                {
                    return;
                }
            }
        }
        Debug.Log("nie ma skrzynki in range");
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectingMaterials : MonoBehaviour
{
    public GameObject my_Player;
    private bool in_Range = false;
    public Animator my_Animator;
    public int object_life;
    public GameObject loot_prefab;
    public List<Loot> loot_list = new List<Loot>();
    public Action OnCollectedCallback;
    private PlayerController playerController;
    private Animator playerAnimator;

    private void Start()
    {
        playerController = my_Player.GetComponent<PlayerController>();
        playerAnimator = my_Player.transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        if (in_Range && Input.GetMouseButtonDown(0))
        {
            Collecting_object();
        }
    }

    public void Collecting_object()
    {
        my_Animator.SetTrigger("Chop");
        if (playerController.isHuman)
        {
            playerAnimator.SetTrigger("chopHuman");
        }
        else
        {
            playerAnimator.SetTrigger("chop");
        }
        object_life--;

        if (object_life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SpawnLoot(transform.position);
        OnCollectedCallback?.Invoke();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            Debug.Log("Weszlo");
            in_Range = true;
        }
    }

    private void OnTriggerExit(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            Debug.Log("WYSZLO");
            in_Range = false;
        }
    }


    List<Loot> DropItemes()
    {
        int random_int = UnityEngine.Random.Range(1, 101);
        List<Loot> final_loot = new List<Loot>();

        foreach (Loot one_item in loot_list)
        {
            if (random_int <= one_item.drop_chance)
            {
                final_loot.Add(one_item);
            }
        }
        return final_loot;
    }

    public void SpawnLoot(Vector3 position)
    {
        List<Loot> final_loot = DropItemes();
        foreach (Loot one_item in final_loot)
        {
            Vector3 spawn_position = new Vector3(position.x + UnityEngine.Random.Range(-1.5f, 1.5f), position.y, position.z + UnityEngine.Random.Range(-1.5f, 1.5f));
            GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);
            spawned_object.GetComponent<PickUp>().my_Player = my_Player;
            spawned_object.GetComponent<PickUp>().item = one_item;
            spawn_position = new Vector3(position.x, position.y, position.z);
            spawned_object.GetComponent<SpriteRenderer>().sprite = one_item.loot_sprite;
        }
    }

    public void DropFromEQ(Loot one_item)
    {
        Debug.Log("jestem w DEQ");
        Vector3 position = my_Player.transform.position;

        Vector3 spawn_position = new Vector3(position.x + UnityEngine.Random.Range(-1.5f, 1.5f), position.y, position.z + UnityEngine.Random.Range(-1.5f, 1.5f));
        GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);
        spawned_object.GetComponent<PickUp>().my_Player = my_Player;
        spawned_object.GetComponent<PickUp>().item = one_item;
        spawn_position = new Vector3(position.x, position.y, position.z);
        spawned_object.GetComponent<SpriteRenderer>().sprite = one_item.loot_sprite;

    }

    public void ResetResource(int deafultLife)
    {
        object_life = deafultLife;
        in_Range = false;

        if (my_Animator != null)
        {
            my_Animator.Rebind();
        }           
    }
}

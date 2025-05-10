using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectingMaterials : MonoBehaviour
{
    public GameObject my_Player;
    private bool in_Range = false;
    public Animator my_Animator;
    public int object_life;
    //public Transform my_Transform;

    public GameObject loot_prefab;
    public List<Loot> loot_list = new List<Loot>();

    //public Collider my_collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (in_Range && Input.GetMouseButtonDown(0))
        {
            Collecting_object();
        }
    }

    public void Collecting_object()
    {
        Debug.Log("Klik³em");
        my_Animator.SetTrigger("Chop");
        object_life--;

        if (object_life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SpawnLoot(transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            Debug.Log("Weszlo");
            in_Range = true;
            Debug.Log(in_Range);
        }
    }

    private void OnTriggerExit(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            Debug.Log("WYSZLO");
            in_Range = false;
            Debug.Log(in_Range);
        }
    }


    List<Loot> DropItemes()
    {
        int random_int = Random.Range(1, 101);
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
        Debug.Log("jestem w spawn");
        List<Loot> final_loot = DropItemes();
        Debug.Log("PO");
        foreach (Loot one_item in final_loot)
        {
            Debug.Log("a ja foreach");
            //float random_range = Random.Range(-1.5f, 1.5f);
            Vector3 spawn_position = new Vector3(position.x + Random.Range(-1.5f, 1.5f), position.y, position.z + Random.Range(-1.5f, 1.5f));
            GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);
            spawn_position = new Vector3(position.x, position.y, position.z);
            spawned_object.GetComponent<SpriteRenderer>().sprite = one_item.loot_sprite;
        }
    }

}

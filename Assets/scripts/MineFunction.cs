using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFunction : MonoBehaviour
{
    public List<Loot> loot_list = new List<Loot>();
    public GameObject my_Player;
    public GameObject loot_prefab;
    private bool in_Range = false;

    public void DropFromMine()
    {
        if (in_Range)
        {
            StartCoroutine(DisappearAndDrop());
        }
    }

    private IEnumerator DisappearAndDrop()
    {
        Vector3 position = my_Player.transform.position;
        Transform playerChild = my_Player.transform.GetChild(0);
        playerChild.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        playerChild.gameObject.SetActive(true);
        my_Player.transform.position = position;
        SpawnLoot();
    }

    List<Loot> ItemesFromMine()
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

    public void SpawnLoot()
    {
        List<Loot> final_loot = ItemesFromMine();
        foreach (Loot one_item in final_loot)
        {
            Vector3 position = my_Player.transform.GetChild(0).position;
            Vector3 spawn_position = new Vector3(position.x + UnityEngine.Random.Range(-1.5f, 1.5f), position.y, position.z + UnityEngine.Random.Range(-0.5f, -1.0f));
            GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);
            spawned_object.GetComponent<PickUp>().my_Player = my_Player;
            spawned_object.GetComponent<PickUp>().item = one_item;
            spawn_position = new Vector3(position.x, position.y, position.z);
            spawned_object.GetComponent<SpriteRenderer>().sprite = one_item.loot_sprite;
        }
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.transform.root.gameObject == my_Player)
        {
            in_Range = true;
        }
    }
    private void OnTriggerExit(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            in_Range = false;
        }
    }
}

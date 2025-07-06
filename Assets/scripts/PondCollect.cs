using System.Collections.Generic;
using UnityEngine;

public class PondCollect : MonoBehaviour
{
    public GameObject my_Player;
    private bool in_Range = false;

    public GameObject loot_prefab;
    public Loot loot;

    public void DropFromPond()
    {
        if (in_Range)
        {
            SpawnLoot(my_Player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
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

    public void SpawnLoot(Vector3 position)
    {
            Vector3 spawn_position = new Vector3(position.x + UnityEngine.Random.Range(-0.5f, 0.5f) - 1.5f, position.y, position.z + UnityEngine.Random.Range(-0.5f, 0.5f) - 3f);
            GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);
            spawned_object.GetComponent<PickUp>().my_Player = my_Player;
            spawned_object.GetComponent<PickUp>().item = loot;
            spawn_position = new Vector3(position.x, position.y, position.z);
            spawned_object.GetComponent<SpriteRenderer>().sprite = loot.loot_sprite;
    }
}

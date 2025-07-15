using UnityEngine;

public class TorchFunction : MonoBehaviour
{
    public GameObject loot_prefab;
    public GameObject my_player;
    public Loot spark;
    public Loot ash;

    void Start()
    {
        InvokeRepeating("GiveSpark", 20, 20);
        Invoke("BurnOut", 60);
    }

    private void GiveSpark()
    {
        Vector3 position = transform.position;
        Vector3 spawn_position = new Vector3(position.x + Random.Range(-1.5f, 1.5f) , position.y, position.z + Random.Range(-1.5f, 1.5f));
        GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);

        spawned_object.GetComponent<PickUp>().my_Player = my_player;
        spawned_object.GetComponent<PickUp>().item = spark;
        spawned_object.GetComponent<SpriteRenderer>().sprite = spark.loot_sprite;
    }

    private void BurnOut()
    {
        Vector3 position = transform.position;
        GameObject ash_object = Instantiate(loot_prefab, position, Quaternion.identity);
        ash_object.GetComponent<PickUp>().my_Player = my_player;
        ash_object.GetComponent<PickUp>().item = ash;
        ash_object.GetComponent<SpriteRenderer>().sprite = ash.loot_sprite;

        Destroy(gameObject);
        my_player.GetComponent<PlayerController>().safeSpace = false;
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_player))
        {
            my_player.GetComponent<PlayerController>().safeSpace = true;
        }
    }

    private void OnTriggerExit(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_player))
        {
            my_player.GetComponent<PlayerController>().safeSpace = false;
        }
    }
}

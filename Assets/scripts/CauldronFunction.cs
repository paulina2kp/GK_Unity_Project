using System.Collections;
using UnityEngine;

public class CauldronFunction : MonoBehaviour
{
    public GameObject loot_prefab;
    public GameObject my_Player;
    private bool in_Range = false;
    public Animator my_Animator;
    private Loot potionToGive;

    public void DropFromCauldron(Loot potion)
    {
        if (in_Range)
        {
            my_Animator.SetTrigger("Make");
            potionToGive = potion;
            StartCoroutine(WaitAnimationAndDrop());

        }
    }
    private IEnumerator WaitAnimationAndDrop()
    {
        yield return new WaitForSeconds(2);
        GivePotion(potionToGive);
    }

    private void GivePotion(Loot potion)
    {
        Vector3 position = transform.position;
        Vector3 spawn_position = new Vector3(position.x + Random.Range(-1.5f, 1.5f), position.y, position.z + Random.Range(-0.5f, -2f));
        GameObject spawned_object = Instantiate(loot_prefab, spawn_position, Quaternion.identity);

        spawned_object.GetComponent<PickUp>().my_Player = my_Player;
        spawned_object.GetComponent<PickUp>().item = potion;
        spawned_object.GetComponent<SpriteRenderer>().sprite = potion.loot_sprite;
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

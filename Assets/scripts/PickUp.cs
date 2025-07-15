using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject my_Player;
    private bool in_Range = false;
    public Loot item;

    void Update()
    {
        if (in_Range)
        {
            Vector3 spritePosition = my_Player.transform.GetChild(0).position;        
            transform.position = Vector3.MoveTowards(transform.position, spritePosition, 1.5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, spritePosition) < 0.5f)
            {
                bool added = Inventory.Instance.Add(item);

                if (added)
                {
                    FindFirstObjectByType<UISlotsHandler>().UpdateInventorySlots();
                    Die();
                }
                else
                {
                    in_Range = false;
                }                    
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            in_Range = true;
        }
    }
}

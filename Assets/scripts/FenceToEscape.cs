using UnityEngine;

public class FenceToEscape : MonoBehaviour
{
    public GameObject my_Player;
    [SerializeField]
    private Loot magicStaff;
    private bool in_Range = false;
    private PlayerController playerController;

    private void Start()
    {
        playerController = my_Player.GetComponent<PlayerController>();
    }

    public void Destroy()
    {
        if (in_Range)
        {
            gameObject.SetActive(false);
        }
        else
        {
            playerController.DropFromEQ(magicStaff);
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

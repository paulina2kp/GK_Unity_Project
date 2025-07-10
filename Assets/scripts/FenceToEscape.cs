using UnityEngine;

public class FenceToEscape : MonoBehaviour
{
    public GameObject my_Player;
    private bool in_Range = false;

    public void Destroy()
    {
        if (in_Range)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        Debug.Log("FENCE WCHODZE");
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

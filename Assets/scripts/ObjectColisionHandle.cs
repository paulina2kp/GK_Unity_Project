using UnityEngine;
using UnityEngine.UIElements;

public class ObjectColisionHandle : MonoBehaviour
{


    public GameObject my_Player;
    private bool in_Range = false;
    public Animator my_Animator;
    public int object_life;

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
            Debug.Log("Klik³em");
            my_Animator.SetTrigger("Chop");
            object_life--;
        }
        
        if (object_life == 0)
        {
            Destroy(gameObject);
        }
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
}

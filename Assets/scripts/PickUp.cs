using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject my_Player;
    private bool in_Range = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (in_Range)
        {
            Vector3 spritePosition = my_Player.transform.GetChild(0).position;
            
            transform.position = Vector3.MoveTowards(transform.position, spritePosition, 1.5f * Time.deltaTime);


            if (Vector3.Distance(transform.position, spritePosition) < 0.5f)
            { 
                 Die();
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
            Debug.Log("moge zbierac");
            in_Range = true;
            Debug.Log(in_Range);
        }
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject my_Player;

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.transform.root.gameObject == my_Player)
        {
            Debug.Log("KONIEC GRY GRATULACJE");
            SceneManager.LoadScene("EndMenu");
        }
    }
}

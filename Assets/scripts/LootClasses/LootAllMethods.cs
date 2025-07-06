using UnityEngine;

public class LootAllMethods : MonoBehaviour
{
    public Loot apple;
    public Loot log;
    public Loot stick;

    private PlayerController playerController;
    private CollectingMaterials collectingMaterials;
    public GameObject player;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        PondCollect[] allPonds = Object.FindObjectsByType<PondCollect>(FindObjectsSortMode.None);
        PondCollect pondWater = GameObject.FindWithTag("Pond").GetComponent<PondCollect>();

        apple.onUse = () => playerController.PlayerEats(5f);
        apple.onDrop = () => playerController.DropFromEQ(apple);

        /*log.onUse = () => { foreach (var pond in allPonds)
                            {
                                pond.DropFromPond(); 
                            }
                          };*/
        log.onUse = () => pondWater.DropFromPond();
        log.onDrop = () => playerController.DropFromEQ(log);

        stick.onUse = () => Debug.Log("stick ON USE");
        stick.onDrop = () => playerController.DropFromEQ(stick);
        
    }


}

using UnityEngine;

public class LootAllMethods : MonoBehaviour
{
    public Loot apple;
    public Loot log;
    public Loot stick;

    private PlayerController playerController;
    private CollectingMaterials collectingMaterials;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        apple.onUse = () => playerController.PlayerEats(5f);
        //apple.onDrop = () => playerController.DamagePlayer(5f);
        apple.onDrop = () => playerController.DropFromEQ(apple);

        log.onUse = () => Debug.Log("LOG ON USE");
        //log.onDrop = () => Debug.Log("LOG ON DROP");
        log.onDrop = () => playerController.DropFromEQ(log);

        stick.onUse = () => Debug.Log("stick ON USE");
        stick.onDrop = () => playerController.DropFromEQ(stick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

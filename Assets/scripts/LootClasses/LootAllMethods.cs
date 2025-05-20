using UnityEngine;

public class LootAllMethods : MonoBehaviour
{
    public Loot apple;
    public Loot log;
    public Loot stick;

    private PlayerController playerController;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        apple.onUse = () => playerController.HealPlayer(5f);
        apple.onDrop = () => playerController.DamagePlayer(5f);
        //apple.onDrop = () => Debug.Log("APPLE ON DROP");

        log.onUse = () => Debug.Log("LOG ON USE");
        log.onDrop = () => Debug.Log("LOG ON DROP");

        stick.onUse = () => Debug.Log("stick ON USE");
        stick.onDrop = () => Debug.Log("stick ON DROP");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

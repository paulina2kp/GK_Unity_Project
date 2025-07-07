using UnityEngine;

public class LootAllMethods : MonoBehaviour
{
    public Loot apple;
    public Loot log;
    public Loot stick;
    public Loot shovel;
    public Loot berry;
    public Loot blueberries;
    public Loot clay;
    public Loot mushroom;
    public Loot toadstool;
    public Loot stone;
    public Loot bowl;
    public Loot waterBowl;
    public Loot sand;
    public Loot torchNotUsed;

    private PlayerController playerController;
    private CollectingMaterials collectingMaterials;
    public GameObject player;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        PondCollect[] allPonds = Object.FindObjectsByType<PondCollect>(FindObjectsSortMode.None);
        PondCollect pondWater = GameObject.FindWithTag("Pond").GetComponent<PondCollect>();

        apple.onDrop = () => playerController.DropFromEQ(apple);
        apple.onUse = () => playerController.PlayerEats(10f);

        log.onDrop = () => playerController.DropFromEQ(log);
        //log.onUse = () => 

        stick.onDrop = () => playerController.DropFromEQ(stick);
        //stick.onUse = () =>      

        shovel.onDrop = () => playerController.DropFromEQ(shovel);
        shovel.onUse = () => 
        {      
            foreach (var pond in allPonds)
            {
                pond.DropFromPond(); 
            }
        };

        berry.onDrop = () => playerController.DropFromEQ(berry);
        berry.onUse = () => playerController.PlayerEats(5f);

        blueberries.onDrop = () => playerController.DropFromEQ(blueberries);
        blueberries.onUse = () => playerController.PlayerEats(5f);

        clay.onDrop = () => playerController.DropFromEQ(clay);
        //clay.onUse = () =>

        mushroom.onDrop = () => playerController.DropFromEQ(mushroom);
        mushroom.onUse = () => playerController.PlayerEats(15f);

        toadstool.onDrop = () => playerController.DropFromEQ(toadstool);
        toadstool.onUse = () => { playerController.DamagePlayer(5f);
                                  playerController.PlayerEats(3f);
                                  playerController.LoseStamina(10f);};

        stone.onDrop = () => playerController.DropFromEQ(stone);
        //stone.onUse = () =>

        bowl.onDrop = () => playerController.DropFromEQ(bowl);
        bowl.onUse = () => pondWater.DropWaterBowl();

        waterBowl.onDrop = () => playerController.DropFromEQ(waterBowl);
        waterBowl.onUse = () => playerController.GetStamina(20f);

        sand.onDrop = () => playerController.DropFromEQ(waterBowl);
        //sand.onUse = () => 

        torchNotUsed.onDrop = () => playerController.DropFromEQ(torchNotUsed);
        //torchNotUsed.onUse = () => 

    }


}

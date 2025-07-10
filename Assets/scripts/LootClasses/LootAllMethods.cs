using UnityEngine;
using static UnityEngine.Rendering.ReloadAttribute;

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
    public GameObject torchPrefab;
    public Loot ashes;
    public Loot spark;
    public Loot flame;
    public Loot glass;
    public Loot emptyPotion;
    public Loot bigStone;
    public Loot bigLog;
    public Loot brick;
    public Loot miniMine;
    public GameObject minePrefab;
    public Loot strongStick;
    public Loot pickaxe;
    public Loot coal;
    public Loot blueGem;
    public Loot purpleGem;
    public Loot yellowGem;
    public Loot redGem;
    public Loot blackGem;
    public Loot magicStone;
    public Loot magicGem;
    public Loot cauldron;
    public GameObject cauldronPrefab;
    public Loot floverBlue;
    public Loot floverPurple;
    public Loot floverYellow;
    public Loot floverRed;
    public Loot floverBlack;
    public Loot dyeRed;
    public Loot dyePurple;
    public Loot dyeBlack;
    public Loot dyeBlue;
    public Loot dyeYellow;
    public Loot potRed;
    public Loot potPurple;
    public Loot potBlack;
    public Loot potBlue;
    public Loot potYellow;
    public Loot magicStaff;

    private PlayerController playerController;
    public GameObject player;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        PondCollect[] allPonds = Object.FindObjectsByType<PondCollect>(FindObjectsSortMode.None);
        PondCollect pondWater = GameObject.FindWithTag("Pond").GetComponent<PondCollect>();

        apple.onDrop = () => playerController.DropFromEQ(apple);
        apple.onUse = () => playerController.PlayerEats(10f);

        log.onDrop = () => playerController.DropFromEQ(log);

        stick.onDrop = () => playerController.DropFromEQ(stick);    

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

        mushroom.onDrop = () => playerController.DropFromEQ(mushroom);
        mushroom.onUse = () => playerController.PlayerEats(15f);

        toadstool.onDrop = () => playerController.DropFromEQ(toadstool);
        toadstool.onUse = () => { playerController.DamagePlayer(5f);
                                  playerController.PlayerEats(3f);
                                  playerController.LoseStamina(10f);};

        stone.onDrop = () => playerController.DropFromEQ(stone);

        bowl.onDrop = () => playerController.DropFromEQ(bowl);
        bowl.onUse = () => pondWater.DropWaterBowl();

        waterBowl.onDrop = () => playerController.DropFromEQ(waterBowl);
        waterBowl.onUse = () => playerController.GetStamina(20f);

        sand.onDrop = () => playerController.DropFromEQ(waterBowl); 

        torchNotUsed.onDrop = () => playerController.DropFromEQ(torchNotUsed);
        torchNotUsed.onUse = () => playerController.PlaceTorch(torchPrefab);

        ashes.onDrop = () => playerController.DropFromEQ(ashes);

        spark.onDrop = () => playerController.DropFromEQ(spark);

        flame.onDrop = () => playerController.DropFromEQ(flame);

        glass.onDrop = () => playerController.DropFromEQ(glass);

        emptyPotion.onDrop = () => playerController.DropFromEQ(emptyPotion);

        bigStone.onDrop = () => playerController.DropFromEQ(bigStone);

        bigLog.onDrop = () => playerController.DropFromEQ(bigLog);

        brick.onDrop = () => playerController.DropFromEQ(brick);

        miniMine.onDrop = () => playerController.DropFromEQ(miniMine);
        miniMine.onUse = () => playerController.PlaceMine(minePrefab);

        strongStick.onDrop = () => playerController.DropFromEQ(strongStick);

        pickaxe.onDrop = () => playerController.DropFromEQ(pickaxe);
        pickaxe.onUse = () =>
        {
            MineFunction[] allMines = Object.FindObjectsByType<MineFunction>(FindObjectsSortMode.None);
            foreach (var mine in allMines)
            {
                if (mine != null)
                {
                    mine.DropFromMine();
                }
            }
        };

        coal.onDrop = () => playerController.DropFromEQ(coal);

        blueGem.onDrop = () => playerController.DropFromEQ(blueGem);
        purpleGem.onDrop = () => playerController.DropFromEQ(purpleGem);
        yellowGem.onDrop = () => playerController.DropFromEQ(yellowGem);
        redGem.onDrop = () => playerController.DropFromEQ(redGem);
        blackGem.onDrop = () => playerController.DropFromEQ(blackGem);

        magicStone.onDrop = () => playerController.DropFromEQ(magicStone);

        magicGem.onDrop = () => playerController.DropFromEQ(magicGem);

        cauldron.onDrop = () => playerController.DropFromEQ(cauldron);
        cauldron.onUse = () => playerController.PlaceCauldron(cauldronPrefab); 

        floverBlue.onDrop = () => playerController.DropFromEQ(floverBlue);
        floverBlack.onDrop = () => playerController.DropFromEQ(floverBlack);
        floverRed.onDrop = () => playerController.DropFromEQ(floverRed);
        floverYellow.onDrop = () => playerController.DropFromEQ(floverYellow);
        floverPurple.onDrop = () => playerController.DropFromEQ(floverPurple);

        dyeBlue.onDrop = () => playerController.DropFromEQ(dyeBlue);
        dyeBlue.onUse = () => 
        {
            CauldronFunction[] allCauldrons = Object.FindObjectsByType<CauldronFunction>(FindObjectsSortMode.None);
            foreach (var cauldron in allCauldrons)
            {
                if (cauldron != null)
                {
                    cauldron.DropFromCauldron(potBlue);
                }
            }
        };

        dyeBlack.onDrop = () => playerController.DropFromEQ(dyeBlack);
        dyeBlack.onUse = () =>
        {
            CauldronFunction[] allCauldrons = Object.FindObjectsByType<CauldronFunction>(FindObjectsSortMode.None);
            foreach (var cauldron in allCauldrons)
            {
                if (cauldron != null)
                {
                    cauldron.DropFromCauldron(potBlack);
                }
            }
        };
        dyeRed.onDrop = () => playerController.DropFromEQ(dyeRed);
        dyeRed.onUse = () => 
        {
            CauldronFunction[] allCauldrons = Object.FindObjectsByType<CauldronFunction>(FindObjectsSortMode.None);
            foreach (var cauldron in allCauldrons)
            {
                if (cauldron != null)
                {
                    cauldron.DropFromCauldron(potRed);
                }
            }
        };
        dyeYellow.onDrop = () => playerController.DropFromEQ(dyeYellow);
        dyeYellow.onUse = () =>
        {
            CauldronFunction[] allCauldrons = Object.FindObjectsByType<CauldronFunction>(FindObjectsSortMode.None);
            foreach (var cauldron in allCauldrons)
            {
                if (cauldron != null)
                {
                    cauldron.DropFromCauldron(potYellow);
                }
            }
        };
        dyePurple.onDrop = () => playerController.DropFromEQ(dyePurple);
        dyePurple.onUse = () =>
        {
            CauldronFunction[] allCauldrons = Object.FindObjectsByType<CauldronFunction>(FindObjectsSortMode.None);
            foreach (var cauldron in allCauldrons)
            {
                if (cauldron != null)
                {
                    cauldron.DropFromCauldron(potPurple);
                }
            }
        };

        potBlue.onDrop = () => playerController.DropFromEQ(potBlue);
        potBlue.onUse = () => playerController.GetStamina(100f);
        potBlack.onDrop = () => playerController.DropFromEQ(potBlack);
        potBlack.onUse = () => { playerController.DamagePlayer(30f);
                                 playerController.SpeedPlayer();};
        potRed.onDrop = () => playerController.DropFromEQ(potRed);
        potRed.onUse = () => playerController.HealPlayer(100f);
        potYellow.onDrop = () => playerController.DropFromEQ(potYellow);
        potYellow.onUse = () => playerController.PlayerEats(100f);
        potPurple.onDrop = () => playerController.DropFromEQ(potPurple);
        //potPurple.onUse = () => TO DO zamiana w czlowieka

        magicStaff.onDrop = () => playerController.DropFromEQ(magicStaff);
        magicStaff.onUse = () => GameObject.FindWithTag("Escape").GetComponent<FenceToEscape>().Destroy();
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Scriptable Objects/Loot")]
public class Loot : ScriptableObject
{
    public string loot_name;
    public Sprite loot_sprite;
    public int drop_chance;
    

    public Loot(string loot_name, Sprite loot_sprite, int drop_chance)
    {
        this.loot_name = loot_name;
        this.loot_sprite = loot_sprite;
        this.drop_chance = drop_chance;
    }
}

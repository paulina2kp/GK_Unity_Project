using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Loot", menuName = "Scriptable Objects/Loot")]
public class Loot : ScriptableObject
{
    public string loot_name;
    public Sprite loot_sprite;
    public int drop_chance;
    public bool isUsable;

    [NonSerialized] public Action onUse;
    [NonSerialized] public Action onDrop;
    
}

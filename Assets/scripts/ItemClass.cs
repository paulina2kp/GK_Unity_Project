using System;

[Serializable]
public class ItemClass
{
    public Loot loot;
    public int stackSize;

    public ItemClass(Loot item)
    {
        loot = item;
        IncStackSize();
    }

    public void IncStackSize()
    {
        stackSize++;
    }

    public void DecStackSize()
    {
        stackSize--;
    }
}

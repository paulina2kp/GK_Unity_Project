using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    private ItemClass currItem;
    public void OnMouseDownItem(ItemClass item)
    {
        if (currItem == null)
        {
            currItem = item;
        }
    }
}

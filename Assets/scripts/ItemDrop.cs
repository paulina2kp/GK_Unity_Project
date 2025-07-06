using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform inventoryPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, eventData.position))
        {
            Debug.Log("DROPUJE");
        }
    }
}

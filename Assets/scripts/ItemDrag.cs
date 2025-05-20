using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Canvas canvas;
    public Transform originalParent;
    public CanvasGroup canvasGroup;

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
        //Debug.Log("ZACZYNAM DRAG");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out pos
        );

        transform.localPosition = pos;
        
        //Debug.Log("DRAGUJE");
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        transform.SetParent(originalParent);

        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
           
        //Debug.Log("KONIEC DRAG");
    }

}

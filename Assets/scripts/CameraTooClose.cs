using System.Collections.Generic;
using UnityEngine;

public class CameraTooClose : MonoBehaviour
{
    public GameObject my_Camera;
    //private bool in_Range = false;
    private SpriteRenderer spriteRenderer;
    private Material mat;

    private void OnTriggerEnter(Collider my_collider)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mat = GetComponent<Renderer>().material;
        if (my_collider.gameObject.Equals(my_Camera))
        {
            Debug.Log("Weszlo KAMERA");
            //in_Range = true;
            //spriteRenderer.enabled = false;
            mat.SetColor("koks", new Color(1, 1, 1, Mathf.Lerp(1, (float)0.5, 10 * Time.deltaTime)));
            Debug.Log("KOlor: " + mat.color);
            //FadeItem();
        }
    }

    private void OnTriggerExit(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Camera))
        {
            Debug.Log("WYSZLO KAMERA");
            //in_Range = false;
            spriteRenderer.enabled = true;
        }
    }

    private void FadeItem()
    {
        Debug.Log("DZIEJE SIE");
        Color currentColor = mat.color;
        Color fadeColor = new Color(currentColor.a, currentColor.b, currentColor.b,
            Mathf.Lerp(currentColor.a, (float)0.5, 10 * Time.deltaTime));
    }
}

using UnityEngine;

public class CraftPanellUI : MonoBehaviour
{
    public GameObject craftPanel;
    bool craftopen = false;

    public void CraftButtonClicked()
    {      
        if (craftPanel != null)
        {
            if (craftopen == false)
            {
                craftPanel.SetActive(true);
                craftopen = true;
            }
            else
            {
                craftPanel.SetActive(false);
                craftopen = false;
            }
        }        
    }
}

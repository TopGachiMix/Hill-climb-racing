using UnityEngine;

public class ClickScripts : MonoBehaviour
{
    public bool clickedIs = false;


    private void OnMouseUp()
    {
        clickedIs = false;

    }
    void OnMouseDown()
    {
        clickedIs = true;
        
    }
}

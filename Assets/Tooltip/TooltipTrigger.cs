using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    [TextArea] public string tooltipText = "Default";

    bool dragging;                // ? track whether the player is holding the object

    void OnMouseEnter()
    {
        if (!dragging)            // only show if we’re NOT dragging
            TooltipUI.Instance?.Show(tooltipText);
    }

    void OnMouseExit() => TooltipUI.Instance?.Hide();

    void OnMouseDown()
    {          // click starts a drag
        dragging = true;
        TooltipUI.Instance?.Hide();
    }

    void OnMouseUp()
    {          // release ends the drag
        dragging = false;
        // optional: immediately show again if the cursor is still over us
        // TooltipUI.Instance?.Show(tooltipText);
    }


    //MORE UI ELEMENTS/BUTTONS
    public void CursorEnter()
    {
        if (!dragging)            // only show if we’re NOT dragging
            TooltipUI.Instance?.Show(tooltipText);
    }

    public void CursorExit()
    {
        TooltipUI.Instance?.Hide();
    }

    public void CursorDown()
    {
        dragging = true;
        TooltipUI.Instance?.Hide();
    }

    public void CursorUp()
    {
        dragging = false;
    }
}
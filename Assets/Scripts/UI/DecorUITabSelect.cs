using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultTabSelector : MonoBehaviour
{
    public Button myButton;

    Color defaultColor;

    bool colorChanged;

    void Start()
    {
        // Get the current ColorBlock
        ColorBlock cb = myButton.colors;

        defaultColor = cb.normalColor;

        // Change the normal color to selected color
        cb.normalColor = new Color(245, 245, 245);

        // Apply the modified ColorBlock back to the button
        myButton.colors = cb;
    }

    public void SetNormalColorToDefault()
    {
        if(!colorChanged)
        {
            ColorBlock cb = myButton.colors;

            cb.normalColor = defaultColor;

            myButton.colors = cb;
        }
    }
}

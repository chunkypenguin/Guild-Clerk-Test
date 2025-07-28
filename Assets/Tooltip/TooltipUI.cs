using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;       // quick singleton

    [SerializeField] TextMeshProUGUI text;


    [SerializeField] Vector2 cursorOffset = new Vector2(20, -16);   
    //[SerializeField] bool clampToScreen = true;

    RectTransform rect;

    void Awake()
    {
        Instance = this;
        rect = GetComponent<RectTransform>();
        Hide();
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        Vector2 pos = Input.mousePosition;

        // put tooltip 12 px right & 16 px down from the cursor
        rect.pivot = new Vector2(0, 1);   
        rect.position = pos + cursorOffset;

        // --- optional screen?edge clamp ---
        float w = rect.rect.width;
        float h = rect.rect.height;

        float x = Mathf.Min(rect.position.x, Screen.width - w);
        float y = Mathf.Max(rect.position.y, h);// keep above bottom edge
        rect.position = new Vector2(x, y);
    }

    public void Show(string msg)
    {
        gameObject.SetActive(true);
        text.text = msg;
    }

    public void Hide() => gameObject.SetActive(false);
}
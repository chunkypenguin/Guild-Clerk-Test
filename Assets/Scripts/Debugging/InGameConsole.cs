using UnityEngine;
using TMPro;

public class InGameConsole : MonoBehaviour
{
    [SerializeField] private TMP_Text consoleText;
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private int maxLogs = 200;

    private System.Collections.Generic.Queue<string> logs = new System.Collections.Generic.Queue<string>();

    void Awake()
    {
        Application.logMessageReceived += HandleLog;
        consolePanel.SetActive(false);
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) // toggle with ~ key
        {
            consolePanel.SetActive(!consolePanel.activeSelf);
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string color;

        switch (type)
        {
            case LogType.Warning:
                color = "yellow";
                break;
            case LogType.Error:
            case LogType.Exception:
                color = "red";
                break;
            default:
                color = "white";
                break;
        }

        string entry = $"<color={color}>{type}: {logString}</color>";

        // Add stack trace for errors/exceptions
        if (type == LogType.Error || type == LogType.Exception)
            entry += $"\n<color=grey>{stackTrace}</color>";

        logs.Enqueue(entry);

        if (logs.Count > maxLogs)
            logs.Dequeue();

        consoleText.text = string.Join("\n", logs);
    }
}


using System.Collections.Generic;
using UnityEngine;

public class ConsoleToSceen : MonoBehaviour
{
    private const int maxLines = 50;
    private const int maxLineLength = 120;
    private string _logStr = "";

    private readonly List<string> _lines = new List<string>();

    private GUIStyle _style;

    private void OnEnable()
    {
        Application.logMessageReceived += OnLogMessageReceived;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= OnLogMessageReceived;
    }

    private void OnLogMessageReceived(string logString, string stackTrace, LogType type)
    {
        foreach (var line in logString.Split('\n'))
        {
            if (line.Length <= maxLineLength)
            {
                _lines.Add(line);
                continue;
            }

            var lineCount = line.Length / maxLineLength + 1;
            for (int i = 0; i < lineCount; i++)
            {
                _lines.Add((i + 1) * maxLineLength <= line.Length
                    ? line.Substring(i * maxLineLength, maxLineLength)
                    : line.Substring(i * maxLineLength, line.Length - i * maxLineLength));
            }
        }

        if (_lines.Count > maxLines)
            _lines.RemoveRange(0, _lines.Count - maxLines);

        _logStr = string.Join("\n", _lines);
    }

    private void OnGUI()
    {
        if (_style == null)
        {
            _style = new GUIStyle(GUI.skin.label);
            _style.fontSize = 28;
            _style.normal.textColor = Color.white;
        }

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
            new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
        GUI.Label(new Rect(10, 10, 800, 370), _logStr, _style);
    }
}
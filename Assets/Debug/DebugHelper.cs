using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DebugHelper is a custom debug visualizer which can be toggled on and off at runtime.
/// </summary>
public static class DebugHelper
{
    public static bool DebugEnabled = true;

    public static void DrawPoint(Vector3 point, Color color)
    {
        if (!DebugEnabled) return;
        Debug.DrawLine(point - new Vector3(0.1f, 0, 0), point + new Vector3(0.1f, 0, 0), color, 0.01f);
        Debug.DrawLine(point - new Vector3(0, 0.1f, 0), point + new Vector3(0, 0.1f, 0), color, 0.01f);

        Debug.DrawLine(point + new Vector3(-0.05f, 0.05f, 0), point + new Vector3(0.05f, 0.05f, 0), color, 0.01f);
        Debug.DrawLine(point + new Vector3(-0.05f, -0.05f, 0), point + new Vector3(0.05f, -0.05f, 0), color, 0.01f);
        Debug.DrawLine(point + new Vector3(-0.05f, -0.05f, 0), point + new Vector3(-0.05f, 0.05f, 0), color, 0.01f);
        Debug.DrawLine(point + new Vector3(0.05f, -0.05f, 0), point + new Vector3(0.05f, 0.05f, 0), color, 0.01f);
    }

    public static void OutlineTile(Vector3Int point, Color color)
    {
        if (!DebugEnabled) return;
        Debug.DrawLine(new Vector3(point.x - 0.5f, point.y + 0.5f, 0), new Vector3(point.x + 0.5f, point.y + 0.5f, 0), color, 0.01f);
        Debug.DrawLine(new Vector3(point.x - 0.5f, point.y - 0.5f, 0), new Vector3(point.x + 0.5f, point.y - 0.5f, 0), color, 0.01f);
        Debug.DrawLine(new Vector3(point.x - 0.5f, point.y - 0.5f, 0), new Vector3(point.x - 0.5f, point.y + 0.5f, 0), color, 0.01f);
        Debug.DrawLine(new Vector3(point.x + 0.5f, point.y - 0.5f, 0), new Vector3(point.x + 0.5f, point.y + 0.5f, 0), color, 0.01f);
    }

    public static void OutlineObject(Vector3 position, Vector3 bounds, Color color)
    {
        if (!DebugEnabled) return;
        Debug.DrawLine(new Vector3(position.x - bounds.x, position.y + bounds.y, 0), new Vector3(position.x + bounds.x, position.y + bounds.y, 0), color, 0.01f);
        Debug.DrawLine(new Vector3(position.x - bounds.x, position.y - bounds.y, 0), new Vector3(position.x + bounds.x, position.y - bounds.y, 0), color, 0.01f);
        Debug.DrawLine(new Vector3(position.x - bounds.x, position.y - bounds.y, 0), new Vector3(position.x - bounds.x, position.y + bounds.y, 0), color, 0.01f);
        Debug.DrawLine(new Vector3(position.x + bounds.x, position.y - bounds.y, 0), new Vector3(position.x + bounds.x, position.y + bounds.y, 0), color, 0.01f);
    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color = default, float time = 0.01f)
    {
        if (!DebugEnabled) return;
        Debug.DrawLine(start, end, color, time);
    }
}

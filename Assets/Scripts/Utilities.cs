using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
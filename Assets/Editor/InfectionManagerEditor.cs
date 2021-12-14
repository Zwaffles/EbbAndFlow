using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InfectionManager))]
public class InfectionManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InfectionManager infectionManager = (InfectionManager)target;

        /* Save values before update */
        int previousWidth = (int)infectionManager.width;
        int previousHeight = (int)infectionManager.height;

        /* Draw Inspector */
        base.OnInspectorGUI();

        /* Load values */
        serializedObject.Update();

        /* Check if values  has changed */
        if (infectionManager.width != previousWidth || infectionManager.height != previousHeight)
        {
            infectionManager.UpdateSpriteShape();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
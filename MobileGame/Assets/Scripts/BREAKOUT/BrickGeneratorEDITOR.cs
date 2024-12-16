using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrickGenerator))]
public class BrickGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Add a button to the Inspector
        BrickGenerator generator = (BrickGenerator)target;
        if (GUILayout.Button("Generate Bricks"))
        {
            generator.GenerateBricks();
        }
    }
}

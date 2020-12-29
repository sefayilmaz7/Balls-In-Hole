using UnityEngine;
using UnityEditor;

public class ColorizeWindow : EditorWindow
{
    Color color;

    [MenuItem("Window/Colorizer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ColorizeWindow>("Colorizer");
    }
    private void OnGUI()
    {
        // Window code for our colorize tool
        GUILayout.Label("Color the selected object in editor!" , EditorStyles.boldLabel);

        color = EditorGUILayout.ColorField("Select Color", color);

        EditorGUILayout.Space();

        if(GUILayout.Button("COLORIZE"))
        {
            Colorize();
        }
    }

    private void Colorize()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial.color = color;
            }
        }
    }

}

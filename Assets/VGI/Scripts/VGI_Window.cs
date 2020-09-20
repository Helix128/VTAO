using UnityEngine;
using UnityEditor;

public class VGI_Window : EditorWindow
{
    GameObject target;
    float rayLength = 1f;
    // Add menu named "My Window" to the Window menu
    [MenuItem("VTAO/Bake Object")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        VGI_Window window = (VGI_Window)EditorWindow.GetWindow(typeof(VGI_Window),false,"VTAO",true);
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Target Object", EditorStyles.boldLabel);
        target = EditorGUILayout.ObjectField(target,typeof(GameObject),true) as GameObject;
        GUILayout.Label("Ray Length", EditorStyles.boldLabel);
        rayLength = EditorGUILayout.Slider(rayLength, 0.5f, 10f);
        if (GUILayout.Button("Bake"))
        {
            if (target != null)
            {
                VGI_Main.BakeObject(target, rayLength);
            }
            else
            {
                Debug.LogWarning("There is no target set,so VTAO was not baked.");
            }
        }
          if (GUILayout.Button("Bake With Children"))
        {
            if (target != null)
            {
                MeshFilter[] childMeshes = target.GetComponentsInChildren<MeshFilter>();
            for (int i = 0; i < childMeshes.Length; i++)
            {
               VGI_Main.BakeObject( childMeshes[i].gameObject,rayLength);
            }
            VGI_Main.BakeObject(target,rayLength);
            }
            else
            {
                Debug.LogWarning("There is no target set,so VTAO was not baked.");
            }
        }
        if (GUILayout.Button("Bake Entire Scene (slowest)"))
        {
            MeshFilter[] childMeshes = FindObjectsOfType<MeshFilter>();
            for (int i = 0; i < childMeshes.Length; i++)
            {
                VGI_Main.BakeObject(childMeshes[i].gameObject,rayLength);
            }
        
        }
    }
}
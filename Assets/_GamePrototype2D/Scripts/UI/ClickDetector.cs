using System.Collections.Generic;
using Pixelplacement;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

//Use backtracking to show the path of the current clicked UIGameObject
public class ClickDetector : Singleton<ClickDetector>
{
    private string currentPath = "";
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position 
                = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)

            {
                GameObject clickedObject = results[0].gameObject;
                PrintHierarchyPath(clickedObject.transform);
                Debug.Log($"TNam - Path: {currentPath.Substring(0, currentPath.Length - 4)}");
                currentPath = "";
            }
        }
    }

    private void PrintHierarchyPath(Transform transform)
    {
        if (transform.parent != null)
        {
            PrintHierarchyPath(transform.parent);
        }
        
        currentPath += $"{transform.name} -> ";
        
    }
}

/*[CustomEditor(typeof(ClickDetector))]
public class ClickDetectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Display the description
        GUILayout.Label("Enable this gameObject if you want to Debug the 'location on the Hierarchy tree' of the game object you just clicked",  new GUIStyle() { wordWrap = true, normal = {textColor = Color.white}} );
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MessageManager))]
public class MessageManagerInspector : Editor
{
    private MessageManager messageManager;
    void OnEnable()
    {
        messageManager = (MessageManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        for (int i = messageManager._keys.Count - 1; i > -1; i--)
            ShowElement(i);
        serializedObject.ApplyModifiedProperties();
    }
    private void ShowElement(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField(messageManager._keys[index].ToString(), EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        foreach (var subcriber in messageManager._values[index])
            EditorGUILayout.LabelField(subcriber.ToString());
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}
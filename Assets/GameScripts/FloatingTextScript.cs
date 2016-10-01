using UnityEngine;
using UnityEditor;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class FloatingTextScript : MonoBehaviour, INote {
    [SerializeField]
    private string m_text;

    public string text {
        get { return m_text; }
        set { m_text = value; }
    }

    public string getNoteString() {
        return m_text;
    }
}

[CustomEditor(typeof(FloatingTextScript), true)]
public class TextField : Editor {
    override public void OnInspectorGUI() {
        EditorUtility.SetDirty(target);

        FloatingTextScript textScript = (FloatingTextScript)target;
        EditorGUILayout.LabelField("Text");
        textScript.text = EditorGUILayout.TextArea(textScript.text, GUILayout.MaxHeight(80));
    }
}
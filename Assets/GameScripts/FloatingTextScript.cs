using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class FloatingTextScript : MonoBehaviour, INote {
    public string m_text;

    public string getNoteString() {
        return m_text;
    }
}

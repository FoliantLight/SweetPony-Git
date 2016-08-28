using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectDestroyer : MonoBehaviour {
    SpawnScript m_spawnScript;

    void Start() {
        m_spawnScript = transform.parent.GetComponent<SpawnScript>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        m_spawnScript.destroyObject(other.gameObject);
    }
}

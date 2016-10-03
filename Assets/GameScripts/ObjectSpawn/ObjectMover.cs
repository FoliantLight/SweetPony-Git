using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMover : MonoBehaviour {
    private float m_speed;
    private Vector3 m_destinationPoint;

    public float speed {
        get { return m_speed; }
        set { m_speed = value; }
    }

    public Vector3 destinationPoint {
        get { return m_destinationPoint; }
        set { m_destinationPoint = value; }
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, m_destinationPoint, m_speed);
    }
}

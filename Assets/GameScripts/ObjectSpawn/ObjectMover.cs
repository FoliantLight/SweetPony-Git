using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour {

    private float m_speed;
    private Vector3 m_destinationPoint;

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, m_destinationPoint, m_speed);
    }

    public void setDestinationPoint(Vector3 point) {
        m_destinationPoint = point;
    }

    public void setSpeed(float speed) {
        m_speed = speed;
    }
}

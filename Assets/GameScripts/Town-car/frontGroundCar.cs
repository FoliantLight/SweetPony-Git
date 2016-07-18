using UnityEngine;
using System.Collections;

public class frontGroundCar : MonoBehaviour {

    [SerializeField] private float speed = 5;
    [SerializeField] private int vector;

    void Start () {
	
	}

    void FixedUpdate()
    {
        transform.position += new Vector3(speed * vector * Time.fixedDeltaTime, 0, 0);
    }
}

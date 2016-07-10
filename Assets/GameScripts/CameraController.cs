using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject player;
    [Range(0,1)][SerializeField] private float dumpTime;
    private Vector3 tempPos;

	void Start () {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        tempPos = Vector3.MoveTowards(transform.position, player.transform.position, dumpTime);
        transform.position = new Vector3(tempPos.x, tempPos.y, -1);
	
	}
}

using UnityEngine; 
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnCarScript : MonoBehaviour {

    [SerializeField]
    private bool isDestroyer = false;

    private BoxCollider2D boxCollider;

    private DateTime lastSpawn;
    private int secToSpawn;
    private System.Random rndSec = new System.Random();

    public GameObject car;

    // Use this for initialization
    void Start () {
	    if (isDestroyer)
        {
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
        }
        secToSpawn = rndSec.Next(1, 5);
    }
	
	// Update is called once per frame
	void Update () {
        if ((DateTime.Now - lastSpawn).TotalSeconds > secToSpawn && !isDestroyer)
        {
            lastSpawn = DateTime.Now;
            secToSpawn = rndSec.Next(1, 5);
            Instantiate(car, transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.transform.tag == "car")
        {
            Destroy(other.gameObject);
        }
    }
}

using UnityEngine; 
using System.Collections.Generic;

public class SpawnScript : MonoBehaviour {
    private Transform m_spawner;
    private Transform m_destroyer;

    public float m_speed;
    private System.Random rnd = new System.Random();

    public GameObject m_objectToSpawn;
    public int m_averageFramesToSpawn;

    HashSet<GameObject> m_spawnedObjects;

    // Use this for initialization
    void Start () {
        m_spawnedObjects = new HashSet<GameObject>();

        m_spawner = transform.FindChild("StartPoint");
        m_destroyer = transform.FindChild("EndPoint");
    }
	
	// Update is called once per frame
	void Update () {
        if (rnd.Next(m_averageFramesToSpawn) == 0) {
            GameObject obj = Instantiate(m_objectToSpawn, m_spawner.position, m_objectToSpawn.transform.rotation) as GameObject;
            obj.GetComponent<ObjectMover>().setSpeed(m_speed);
            obj.GetComponent<ObjectMover>().setDestinationPoint(m_destroyer.transform.position);
            m_spawnedObjects.Add(obj);
        }
    }

    public void destroyObject(GameObject obj) {
        if(m_spawnedObjects.Contains(obj)) {
            Destroy(obj);
        }
    }
}

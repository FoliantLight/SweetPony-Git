using UnityEngine; 
using System.Collections.Generic;

public class SpawnScript : MonoBehaviour {
    private Transform m_spawner;
    private Transform m_destroyer;

    private HashSet<GameObject> m_spawnedObjects;

    private System.Random rnd = new System.Random();

    public GameObject m_objectToSpawn;
    public float m_speed;
    public int m_averageFramesToSpawn;
    public int m_maximumObjects;
    public bool m_flip;

    // Use this for initialization
    void Start () {
        m_spawnedObjects = new HashSet<GameObject>();

        m_spawner = transform.FindChild("StartPoint");
        m_destroyer = transform.FindChild("EndPoint");
    }
	
	// Update is called once per frame
	void Update () {
        if (rnd.Next(m_averageFramesToSpawn) == 0 && m_spawnedObjects.Count < m_maximumObjects) {
            GameObject obj = Instantiate(m_objectToSpawn, m_spawner.position, m_objectToSpawn.transform.rotation) as GameObject;
            obj.GetComponent<ObjectMover>().speed = m_speed;
            obj.GetComponent<ObjectMover>().destinationPoint = m_destroyer.transform.position;
            m_spawnedObjects.Add(obj);

            if(m_flip) {
                obj.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public void destroyObject(GameObject obj) {
        if(m_spawnedObjects.Contains(obj)) {
            Destroy(obj);
        }
        m_spawnedObjects.Remove(obj);
    }
}

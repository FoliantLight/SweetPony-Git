using UnityEngine;
using System.Collections;

public class ActionItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        mpac.isUseAroundEnter(transform.gameObject);
        transform.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        mpac.isUseAroundExit();
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

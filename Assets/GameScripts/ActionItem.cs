using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ActionItem : MonoBehaviour {

    private BoxCollider2D boxCollider;
    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
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

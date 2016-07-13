using UnityEngine;
using System.Collections;

public class teleport : ActionItem
{
    Transform insideHouse;
    Transform outsideHouse;
    Transform insideFloor;

    //Переменная, которая отвечает за перемещение между домом и улицей
    private bool inHouse = false;

    // Use this for initialization
    void Start () {
        outsideHouse = transform.GetChild(3);

        outsideHouse.position = new Vector3(outsideHouse.position.x, outsideHouse.position.y, 0);

        insideHouse = transform.GetChild(0);
        insideFloor = transform.GetChild(1).GetChild(0);
        
    }

    public void goToHouse(GameObject other)
    {
        if(!inHouse)
        {
            other.transform.position = insideHouse.position;
            insideFloor.gameObject.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            inHouse = true;
        }
        else
        {
            other.transform.position = outsideHouse.position;
            insideFloor.gameObject.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
            inHouse = false;
        }
        
    }
}

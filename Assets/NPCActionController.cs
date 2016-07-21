using UnityEngine;
using System.Collections;

public class NPCActionController : ActionItem {
    public string name = "";
    void OnTriggerEnter2D(Collider2D other)
    {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        var NPC = new NPC(name); // временно! для тестирования

       // transform.GetComponent<SpriteRenderer>()
    }

    void OnTriggerExit2D(Collider2D other)
    {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
    }
}

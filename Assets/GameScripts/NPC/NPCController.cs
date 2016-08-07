using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class NPCController : ActionItem
{
    public string name = ""; 
    void OnTriggerEnter2D(Collider2D other) {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        mpac.setActionItem(this);
        transform.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void OnTriggerExit2D(Collider2D other) {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        mpac.unsetActionItem();
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public override void triggerAction() {
        
    }
}


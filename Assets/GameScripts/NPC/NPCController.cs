using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class NPCController :ActionItem
{
    public string name = ""; 
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


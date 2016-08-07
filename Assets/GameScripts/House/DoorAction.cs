using UnityEngine;
using System.Collections;

public class DoorAction : ActionItem {
    //Передняя часть дома;
    private Transform m_front;
    //Внутренние коллидеры дома
    private Transform m_insideColliders;
    //Точка входа внутри дома
    private Transform m_inPoint;
    //Точка входа вне дома
    private Transform m_outPoint;
    //Переменная, которая отвечает за перемещение между домом и улицей
    private bool inHouse = false;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        Transform house = transform.parent;

        m_front = house.FindChild("Front");
        m_insideColliders = house.FindChild("InsideColliders");
        m_inPoint = transform.FindChild("InPoint");
        m_outPoint = transform.FindChild("OutPoint");

        if(m_insideColliders != null) {
            m_insideColliders.gameObject.SetActive(false);
        }
    }

    public override void triggerAction() {
        if(inHouse) {
            m_front.gameObject.SetActive(true);
            m_insideColliders.gameObject.SetActive(false);
            m_mainPersonScript.transform.position =
                    new Vector3(m_mainPersonScript.transform.position.x, m_outPoint.position.y, 0.0F);
            inHouse = false;
        }
        else {
            m_front.gameObject.SetActive(false);
            m_insideColliders.gameObject.SetActive(true);
        m_mainPersonScript.transform.position =
                new Vector3(m_mainPersonScript.transform.position.x, m_inPoint.position.y, 0.0F);
            inHouse = true;
        }
    }
}

using UnityEngine;
using System.Collections;

public class DoorAction : ActionItem {
    //Передняя часть дома, которая убирается;
    private Transform m_front;
    //Внутренние коллидеры дома
    protected Transform m_insideColliders;
    //Точка входа внутри дома
    protected Transform m_inPoint;
    //Точка входа вне дома
    protected Transform m_outPoint;

    //Дом
    protected Transform m_house;

    //Слой отрисовки персонажа внутри дома
    public string m_playerSortingLayer;
    //Сохранение горизонтальной координаты при перемещении
    public bool m_keepXCoordinate;

    //Переменная, которая отвечает за перемещение между домом и улицей
    protected bool m_inHouse = false;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        m_house = transform.parent;
        m_front = m_house.FindChild("Front");
        m_insideColliders = m_house.FindChild("InsideColliders");
        m_inPoint = transform.FindChild("InPoint");
        m_outPoint = transform.FindChild("OutPoint");

        if(m_insideColliders != null) {
            m_insideColliders.gameObject.SetActive(false);
        }
    }

    public override void triggerAction() {
        float xPos;
        float yPos;

        if(m_inHouse) {
            getOutsideHouse();
            xPos = m_outPoint.position.x;
            yPos = m_outPoint.position.y;
        }
        else {
            getInsideHouse();
            xPos = m_inPoint.position.x;
            yPos = m_inPoint.position.y;
        }

        if(m_insideColliders != null) {
            m_insideColliders.gameObject.SetActive(!m_inHouse);
        }
        if(m_keepXCoordinate) {
            xPos = m_mainPersonScript.transform.position.x;
        }
        m_mainPersonScript.transform.position = new Vector3(xPos, yPos, 0.0F);
        m_inHouse = !m_inHouse;
    }

    public virtual void getInsideHouse() {
        if(m_front != null) {
            m_front.gameObject.SetActive(false);
        }
        m_mainPersonScript.GetComponent<SpriteRenderer>().sortingLayerName = m_playerSortingLayer;
    }

    public virtual void getOutsideHouse() {
        if(m_front != null) {
            m_front.gameObject.SetActive(true);
        }
        m_mainPersonScript.GetComponent<SpriteRenderer>().sortingLayerName = SortingLayers.Player;
    }
}

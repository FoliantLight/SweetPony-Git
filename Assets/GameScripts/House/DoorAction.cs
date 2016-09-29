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
        if(m_inHouse) {
            getOutsideHouse();
            if(m_insideColliders != null) {
                m_insideColliders.gameObject.SetActive(false);
            }

            m_mainPersonScript.transform.position = new Vector3(m_outPoint.position.x, m_outPoint.position.y, 0.0F);
            m_inHouse = false;
        }
        else {
            getInsideHouse();

            if(m_insideColliders != null) {
                m_insideColliders.gameObject.SetActive(true);
            }

            m_mainPersonScript.transform.position = new Vector3(m_inPoint.position.x, m_inPoint.position.y, 0.0F);
            m_inHouse = true;
        }
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

    #region InterfaceTest implementation
    public void showText() {
        Debug.Log("O_o");
    }
    #endregion
}

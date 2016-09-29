using UnityEngine;
using System.Collections;

public class MainHouseDoor : DoorAction {

    private Transform m_playersFlat;

	// Use this for initialization
    protected override void Start () {
        base.Start();
        m_playersFlat = m_house.FindChild("PlayersFlat");
        m_playersFlat.gameObject.SetActive(false);
	}
	
    public override void getInsideHouse() {
        m_playersFlat.gameObject.SetActive(true);
    }

    public override void getOutsideHouse() {
        m_playersFlat.gameObject.SetActive(false);
    }
}

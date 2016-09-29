using UnityEngine;
using System.Collections;

public class InventoryCanvas : MonoBehaviour {
    void Start () {
        transform.GetChild(Inventories.OthersInventory).gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slotPrefab;
    void Start()
    {
        InventorySystemTest.instance.onInventoryChangedEvent += OnUpdateInventory;
    }
    private void OnUpdateInventory()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }
    public void DrawInventory()
    {
        foreach(InventoryItem item in InventorySystemTest.instance.inventory)
        {
            AddInventorySlot(item);
        }
    }
    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);

        DisplayInventory slot = obj.GetComponent<DisplayInventory>();
        slot.Set(item);
    }
}

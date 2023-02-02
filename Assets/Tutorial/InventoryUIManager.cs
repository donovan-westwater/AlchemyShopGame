using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    // Assoiated Inventory
    public InventorySystemTest backend;
    // Start is called before the first frame update
    public GameObject slotPrefab;
    void Start()
    {
        backend.onInventoryChangedEvent += OnUpdateInventory;
    }
    public void OnUpdateInventory()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }
    public void DrawInventory()
    {
        foreach(InventoryItem item in backend.inventory)
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
        slot.SetInventory(backend);
    }
}

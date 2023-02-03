using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI Label;

    [SerializeField]
    private GameObject stack;

    [SerializeField]
    private TextMeshProUGUI stackLabel;

    private InventorySystem parentInv;

    private InventoryItem reference;
    public void Set(InventoryItem item)
    {
        icon.sprite = item.data.icon;
        Label.text = item.data.name;
        reference = item;
        if (item.stackSize <= 1)
        {
            stack.SetActive(false);
            return;
        }
        stackLabel.text = item.stackSize.ToString(); 
    }
    public void SetInventory(InventorySystem t)
    {
        parentInv = t;
    }
    //Transfers to first active inventory it can find
    public void TransferClick()
    {
        if (parentInv.isPlayerInv)
        {
            foreach(InventorySystem t in InventorySystem.instanceList)
            {
                if (t.isPlayerInv == false && t.enabled)
                {
                    //remove from parent
                    parentInv.Remove(reference.data);
                    //add to first active
                    t.Add(reference.data);
                    return;
                }
            }
        }
        else
        {
            parentInv.playerInv.GetComponent<InventorySystem>().Add(reference.data);
            parentInv.Remove(reference.data);
        }
    }
    /*
    public void OnClickTest()
    {
        if (this.gameObject.transform.IsChildOf(InventorySystemTest.instance.gameObject.transform))
        {
            this.gameObject.transform.SetParent(InventorySystemTest.instance.playerInv.transform);

        }
        else
        {
            this.gameObject.transform.SetParent(InventorySystemTest.instance.transform);
        }
        //Debug.Log("Gay rights baybeeeeee");
    }
    */
}

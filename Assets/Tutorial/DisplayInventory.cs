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
    public void Set(InventoryItem item)
    {
        icon.sprite = item.data.icon;
        Label.text = item.data.name;
        if(item.stackSize <= 1)
        {
            stack.SetActive(false);
            return;
        }
        stackLabel.text = item.stackSize.ToString();
    }
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
}

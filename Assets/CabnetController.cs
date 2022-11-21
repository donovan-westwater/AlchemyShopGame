using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabnetController : MonoBehaviour
{
    public PlayerController player;
    public PlayerInventory inv;
    public GameObject panel;
    [SerializeField]
    GameObject ingredientPrefab;
    // Start is called before the first frame update
    public GameObject[] inventory = new GameObject[16]; //Should be prefabs, but aren't right now
    int label = 1;
    void Start()
    {
        int shift = Random.Range(0, 3);
        label += 1 + shift;
        
        for(int i = 0; i < 16; i++)
        {
            inventory[i] = GameObject.Instantiate(ingredientPrefab);
            inventory[i].GetComponent<Ingredient>().Init(label, CabinetInventory.instance.gameObject);
            inventory[i].transform.SetParent(CabinetInventory.instance.gameObject.transform);
            inventory[i].SetActive(false);
            shift = Random.Range(0, 3);
            label += 1 + shift;
        }
    }
    
    // Update is called once per frame
    //TODO: Move this to use a OnTriggerExit / Enter instead of update
    void Update()
    {
        if(Vector3.Distance(this.transform.position,player.transform.position) < 1.1f)
        {
            if(CabinetInventory.currentInv == null) CabinetInventory.currentInv = this;
            panel.SetActive(true);
            foreach(GameObject o in inventory)
            {
                if(o != null)o.SetActive(true);
            }
        }
        else
        {
            CabinetInventory.currentInv = null;
            foreach (GameObject o in inventory)
            {
                if (o != null) o.SetActive(false);
            }
            panel.SetActive(false);
        }
    }
    public void RemoveIngredient(GameObject o)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].Equals(o))
            {
                GameObject.Destroy(o);
                inventory[i] = null;
                return;
            }
        }
    }
    //returns null when full
    public GameObject AddIngredient(int type,bool isVisible)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                GameObject o = GameObject.Instantiate(ingredientPrefab);
                inventory[i] = o;
                inventory[i].GetComponent<Ingredient>().Init(type, CabinetInventory.instance.gameObject);
                inventory[i].transform.SetParent(CabinetInventory.instance.gameObject.transform);
                inventory[i].SetActive(isVisible);
                return inventory[i];
            }
        }
        return null;
    }
    public GameObject AddIngredient(GameObject o,bool isVisible)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = o;
                inventory[i].transform.SetParent(CabinetInventory.instance.gameObject.transform);
                inventory[i].SetActive(isVisible);
                return inventory[i];
            }
        }
        return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler
{
    public static PlayerInventory instance = null;
    public GameObject[] inventory = new GameObject[4];
    [SerializeField]
    GameObject ingredientPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < inventory.Length; i++) inventory[i] = null;
    }
    public void RemoveIngredient(GameObject o)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].Equals(o)) {
                GameObject.Destroy(o);
                inventory[i] = null;
                return;
            }
        }
    }
    //returns null when full
    public GameObject AddIngredient(int type)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null && instance.gameObject != null)
            {
                GameObject o = GameObject.Instantiate(ingredientPrefab);
                inventory[i] = o;
                inventory[i].GetComponent<Ingredient>().Init(type, instance.gameObject);
                inventory[i].transform.SetParent(instance.gameObject.transform);
                inventory[i].SetActive(true);
                return inventory[i];
            }
        }
        return null;
    }
    public GameObject AddIngredient(GameObject o, bool isVisible)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                o.transform.SetParent(instance.gameObject.transform);
                inventory[i] = o;
                inventory[i].transform.SetParent(instance.gameObject.transform);
                inventory[i].SetActive(isVisible);
                return inventory[i];
            }
        }
        return null;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.tag == "Ingredient") return;
        Ingredient.inventory = this.gameObject;
        Debug.Log("Player ENTER " + Ingredient.inventory.name + " " + eventData.pointerEnter.gameObject.tag);
    }
    //Check if hover over inventory and remove it [Might be good to move to inventory section?]
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Player EXIT");
        //Ingredient.inventory = null;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    [SerializeField] Item test;

    public void Purchase()
    {
        Debug.Log(GameManager.instance.Money);
        Debug.Log(test.ItemData.Price);

        if (GameManager.instance.Money >= test.ItemData.Price)
        {
            GameManager.instance.Money -= test.ItemData.Price;
            Inventory.instance.AddItem(test);
        }
        else
        {
            Debug.Log("������");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public bool isInInventory = false;
    public int itemCount = 0;

    public bool isInCraft = false;
    public int craftCount = 0;
}

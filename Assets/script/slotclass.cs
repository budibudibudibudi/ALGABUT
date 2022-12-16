using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class slotclass
{
    [SerializeField] itemclass item;
    public int stock;

    public slotclass()
    {
        item = null;
        stock = 0;
    }
    public slotclass(itemclass _item, int _stock)
    {
        item = _item;
        stock = _stock;
    }
    public void clear()
    {
        item = null;
        stock = 0;
    }
    public itemclass getitem() { return item; }
    public int getstock() { return stock; }
    public void addstock(int _stock) { stock = Mathf.Clamp(stock + _stock, 0, 99); }
    public void substock(int _stock) { stock = Mathf.Clamp(stock - _stock, 0, 99); }
    public void additem(itemclass _item, int _stock)
    {
        item = _item;
        stock = _stock;
    }
}

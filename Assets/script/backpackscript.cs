using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class backpackscript : MonoBehaviour
{
    public float rotY;
    //itemclass yg sedang dipake
    public itemclass mainitem;
    //itemclass dan ui backpack
    public slotclass[] item;
    public GameObject[] itembackpack;

    public int useditem = -1; //index item yg sedang dipakai
    bool[] isusingitem;// guna buat unuse item
    GameObject[] pintus; //pintu yg ada di map
    public GameObject[] items; //items yg ada di map yg bisa diambil
    float distance = 5; //jarak antara player dengan item/pintu
    public GameObject interactalert; //sesuai nama
    private void Start()
    {
        isusingitem = new bool[4];
        //pintus = GameObject.FindGameObjectsWithTag("pintu");
        //items = GameObject.FindGameObjectsWithTag("item");
        item = new slotclass[4];
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = new slotclass();
        }

        refreshui();
    }
    private void Update()
    {
        rotY = transform.localEulerAngles.y;

        pintus = GameObject.FindGameObjectsWithTag("pintu");
        items = GameObject.FindGameObjectsWithTag("item");

        if (Input.GetKeyDown("1"))
        {
            usingiteminbackpack(0);
        }
        if (Input.GetKeyDown("2"))
        {
            usingiteminbackpack(1);
        }
        if (Input.GetKeyDown("3"))
        {
            usingiteminbackpack(2);
        }
        if (Input.GetKeyDown("4"))
        {
            usingiteminbackpack(3);
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                if (Vector3.Distance(items[i].transform.position, transform.position) <= distance)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        FindObjectOfType<backpackscript>().add(Resources.Load<itemclass>("itemclass/" + items[i].gameObject.name), 1);
                        Destroy(items[i].gameObject);
                        items[i] = null;
                    }
                }

            }

        }

        for (int i = 0; i < pintus.Length; i++)
        {
            if (pintus[i] != null)
            {
                if (Vector3.Distance(pintus[i].transform.position, transform.position) <= 7)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (mainitem != null)
                        {
                            if (pintus[i].GetComponent<pinturequire>().requirement == mainitem.nama)
                            {
                                pintus[i].transform.parent.GetComponent<pintuscript>().isopen = true;
                            }
                            else if (pintus[i].GetComponent<pinturequire>().requirement == null)
                            {
                                Debug.Log("require belum diisi");
                            }
                            else
                                Debug.Log("item tidak dapat digunakan");
                        }
                        else
                        {
                            Debug.Log("tidak ada item yang digunakan");
                        }
                    }
                }
            }

            if(mainitem != null)
                if(Input.GetKeyDown(KeyCode.X))
                {
                    sub(mainitem, 1);
                }

        }
    }


    void usingiteminbackpack(int index)
    {
        if(isusingitem[index])
        {
            itembackpack[useditem].GetComponent<Outline>().enabled = false;
            isusingitem[useditem] = false;
            useditem = -1;
            mainitem = null;
        }
        else
        {
            if (useditem <= -1)
            {
                useditem = index;
                isusingitem[useditem] = true;
                itembackpack[useditem].GetComponent<Outline>().enabled = true;
                if (item[useditem] != null)
                {
                    mainitem = item[useditem].getitem();
                }
                else
                    mainitem = null;
            }
            else
            {
                itembackpack[useditem].GetComponent<Outline>().enabled = false;
                isusingitem[useditem] = false;
                useditem = index;
                itembackpack[useditem].GetComponent<Outline>().enabled = true;
                isusingitem[useditem] = true;
                if (item[useditem] != null)
                {
                    mainitem = item[useditem].getitem();
                }
                else
                    mainitem = null;
            }

        }
        refreshui();
    }
    public void refreshui()
    {
        for (int i = 0; i < itembackpack.Length; i++)
        {
           try
            {
                itembackpack[i].GetComponent<Image>().sprite = item[i].getitem().item.GetComponent<SpriteRenderer>().sprite;
                itembackpack[i].transform.GetChild(0).GetComponent<Text>().text = item[i].getstock().ToString();
            }
            catch
            {
                itembackpack[i].GetComponent<Image>().sprite = null;
                itembackpack[i].transform.GetChild(0).GetComponent<Text>().text = " ";
            }
        }
    }
    public bool add(itemclass _item, int quantity)
    {
        slotclass slot = contains(_item);
        if (slot != null)
        {
            if (slot.getitem().isstackable)
            {
                slot.addstock(quantity);
            }

            else
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i].getitem() == null)
                    {
                        item[i].additem(_item, quantity);
                        break;
                    }

                }
            }
        }
        else
        {
            for (int i = 0; i < item.Length; i++)
            {
                if (item[i].getitem() == null)
                {
                    item[i].additem(_item, quantity);
                    break;
                }

            }

        }
        refreshui();
        return true;

    }
    public bool sub(itemclass _item, int quantity)
    {
        slotclass slot = contains(_item);
        if (slot != null)
        {
            slot.substock(quantity);
            if (transform.localEulerAngles.y == 0)
            {
                Instantiate(slot.getitem().item, new Vector2(this.transform.position.x - 2, this.transform.position.y), Quaternion.identity);
            }
            else if (transform.localEulerAngles.y == 180)
            {
                Instantiate(slot.getitem().item, new Vector2(this.transform.position.x + 2, this.transform.position.y), Quaternion.identity);
            }

            if (slot.getstock() == 0)
            {
                slot.clear();
            }
            else
                return false;
        }
        else
        {
            return false;

        }
        refreshui();
        return true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "pintu")
        {
            interactalert.SetActive(true);
            interactalert.GetComponent<TextMeshPro>().text = "F interact";

        }
        if(collision.gameObject.tag == "item")
        {
            interactalert.SetActive(true);
            interactalert.GetComponent<TextMeshPro>().text = "E interact";
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            interactalert.SetActive(false);
        }
        if (collision.gameObject.tag == "pintu")
        {
            interactalert.SetActive(false);
        }

    }
    public slotclass contains(itemclass _item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].getitem() == _item)
                return item[i];
        }
        return null;
    }
}

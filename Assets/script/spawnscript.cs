using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class spawnscript : MonoBehaviour
{
    public GameObject musuh;
    public int level = 1;
    public int maxspawn = 3;
    public int currentspawn;
    public int minspawn;
    float startspawnsec = 3f;
    Vector2[] spawnpos;

    // Start is called before the first frame update
    void Start()
    {
        spawnpos = new Vector2[4];
        minspawn = maxspawn;
        startspawn();
    }
    void startspawn()
    {
        InvokeRepeating("spawning", startspawnsec, 1);
    }
    void spawning()
    {
        spawnpos[0] = new Vector2(-37.8f, 17.6f);
        spawnpos[1] = new Vector2(37.8f, 17.6f);
        spawnpos[2] = new Vector2(37.8f, -17.6f);
        spawnpos[3] = new Vector2(-37.8f, -17.6f);
        int rand = Random.Range(0, spawnpos.Length);
        if (currentspawn == maxspawn)
        {
            CancelInvoke();
            int random = Random.Range(0, currentspawn);
            GameObject[] a = GameObject.FindGameObjectsWithTag("enemy");
            a[random].GetComponent<enemyscript>().sus = true;

        }
        else
        {
            Instantiate(musuh, spawnpos[rand], Quaternion.identity);
            currentspawn++;
        }
    }
    private void Update()
    {
        if(minspawn <= 0)
        {
            level++;
            if (level >= 10)
                startspawnsec = 2;
            currentspawn = 0;
            maxspawn++;
            minspawn = maxspawn;
            FindObjectOfType<gamemanage>().levelteks.text = "Level : "+level.ToString();
            startspawn();
        }
    }
}

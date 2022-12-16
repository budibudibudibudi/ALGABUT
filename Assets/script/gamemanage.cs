using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gamemanage : MonoBehaviour
{
    public static int score;
    public Text scoreteks;
    public Text levelteks;
    public GameObject Player;
    public GameObject canvass;
    public GameObject canvasselect;
    public GameObject tempcamera;
    public GameObject chosecar;
    public GameObject chosedev;
    public GameObject gameovercanvas;
    public bool ishunter;
    public bool ispc;
    public Text fpsText;
    public float deltaTime;

    private void Awake()
    {
        Application.targetFrameRate = 60;

    }
    private void Start()
    {
        scoreteks.text = "0";
        levelteks.text += "1";
        fpsText.text = "";
        if (PlayerPrefs.HasKey("playertype"))
        {
            Player = Resources.Load<GameObject>("prefab/" + PlayerPrefs.GetString("playertype"));
            Instantiate(Player, transform.position, Quaternion.identity);
        }
        else
            return;
    }
    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
        scoreteks.text = score.ToString();
    }
    public void chosingdevice(string devicestring)
    {
        if (devicestring == "PC")
            ispc = true;
        else if(devicestring == "Android")
            ispc = false;

        Destroy(chosedev);
        chosecar.SetActive(true);
    }
    public void gameover()
    {
        tempcamera.SetActive(true);
        gameovercanvas.SetActive(true);
    }

    public void movescene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void chosingCharacter(string playerselected)
    {
        if (playerselected == "Player_Hunter")
            ishunter = true;
        //FindObjectOfType<spawnscript>().enabled = true;
        Player = Resources.Load<GameObject>("prefab/" + playerselected);
        Instantiate(Player, transform.position, Quaternion.identity);
        PlayerPrefs.SetString("playertype", playerselected);
        canvass.SetActive(true);
        Destroy(canvasselect);
        tempcamera.SetActive(false); 
    }

//skill area
    public void skill1()
    {

    }    
}

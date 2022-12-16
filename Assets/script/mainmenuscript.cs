using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mainmenuscript : MonoBehaviour
{
    public InputField nameinput;
    public GameObject errortext;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void playgame()
    {
        if (nameinput.text == "")
            StartCoroutine(errormsg());
        else
        {
            PlayerPrefs.SetString("name", nameinput.text);
            Debug.Log(PlayerPrefs.GetString("name"));
            SceneManager.LoadScene(1);
        }
    }
    IEnumerator errormsg()
    {
        errortext.SetActive(true);
        yield return new WaitForSeconds(3);
        errortext.SetActive(false);
    }
    public void quitgame()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class playerscript : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    public bool facingright = false;
    public FloatingJoystick fj;
    public TMP_Text playername;
    Animator anim;
    public bool attacking;
    public int currenthealth;
    public int maxhealth = 10;
    public Slider healthbar;
    AudioSource aaudio;
    public AudioClip[] sfx;
    public weapon hunterweapon;
    public Transform shootpoint;
    public GameObject phonecontroller;
    public backpackscript backpackscript;

    private void Awake()
    {

        if (!FindObjectOfType<gamemanage>().ispc)
        {
            phonecontroller.SetActive(true);

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playername.text = PlayerPrefs.GetString("name");
        PlayerPrefs.DeleteKey("name");
        currenthealth = maxhealth;
        healthbar.value = maxhealth;
        aaudio = GetComponent<AudioSource>();
        if (hunterweapon == null)
            return;
    }
    private void Update()
    {
        if (FindObjectOfType<gamemanage>().ispc)
            commandattackpc();


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            attacking = true;
        else
            attacking = false;

    }

    public void commandattackphone()
    {
        if (!attacking)
        {
            if (hunterweapon != null)
                hunterweapon.shoot();
            anim.SetTrigger("Attack");

        }
        else
            return;
        AudioSource a = gameObject.AddComponent<AudioSource>();
        a.PlayOneShot(sfx[0]);
    }
    void commandattackpc()
    {
        if (!attacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hunterweapon != null)
                    hunterweapon.shoot();
                AudioSource a = gameObject.AddComponent<AudioSource>();
                a.PlayOneShot(sfx[0]);
                anim.SetTrigger("Attack");
            }

        }
        else
            return;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //pc
        if (FindObjectOfType<gamemanage>().ispc)
            movepc();
        //smartphone
        else
            movephone();
    }
    void movephone()
    {
        Vector2 direction = Vector2.up * fj.Vertical + Vector2.right * fj.Horizontal;
        rb.velocity = direction * speed * Time.fixedDeltaTime;
        if (fj.Vertical > 0 || fj.Vertical < 0 || fj.Horizontal > 0 || fj.Horizontal < 0)
        {
            anim.SetInteger("AnimState", 2);
            if (!aaudio.isPlaying)
            {
                aaudio.PlayOneShot(sfx[1]);
                aaudio.loop = true;
            }

        }
        else
        {
            anim.SetInteger("AnimState", 0);
            aaudio.Stop();
            aaudio.loop = false;
        }

    }
    void movepc()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(x * speed * Time.fixedDeltaTime, y * speed * Time.fixedDeltaTime);
        if (x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            playername.transform.localEulerAngles = new Vector3(0, 180, 0);
            backpackscript.interactalert.transform.localEulerAngles = new Vector3(0, 180, 0);
            backpackscript.interactalert.transform.localPosition = new Vector3(-1.2f, 1.2f, 0);
        }

        else if (x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            playername.transform.localEulerAngles = new Vector3(0, 0, 0);
            backpackscript.interactalert.transform.localEulerAngles = new Vector3(0, 0, 0);
            backpackscript.interactalert.transform.localPosition = new Vector3(1.2f, 1.2f, 0);
        }

        if (x > 0 || x < 0 || y > 0 || y < 0)
        {
            anim.SetInteger("AnimState", 2);
            if (!aaudio.isPlaying)
            {
                aaudio.PlayOneShot(sfx[1]);
                aaudio.loop = true;
            }
        }
        else
        {
            anim.SetInteger("AnimState", 0);
            aaudio.Stop();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")    
        {
            if(!FindObjectOfType<gamemanage>().ishunter)
            {
                if (attacking)
                {
                    Destroy(collision.gameObject);
                    gamemanage.score++;
                }
                else
                {
                    Destroy(collision.gameObject);
                    changehealth(-1);
                }
            }
            else
            {
                Destroy(collision.gameObject);
                changehealth(-1);
            }
            anim.SetTrigger("Hurt");
            FindObjectOfType<spawnscript>().minspawn--;
        }
        if(collision.gameObject.tag == "nyawa")
        {
            changehealth(1);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "pintu2")
        {
            if (collision.gameObject.GetComponent<pinturequire>().requirement != null)
                SceneManager.LoadScene(collision.gameObject.GetComponent<pinturequire>().requirement);
            else
                Debug.Log("requirement pintu belum diisi");
        }
    }
    public void changehealth(int amount)
    {
        currenthealth = Mathf.Clamp(currenthealth + amount, 0, maxhealth);
        healthbar.value = currenthealth;
        if (currenthealth <= 0)
        {
            anim.SetTrigger("Death");
            speed = 0;
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Destroy(FindObjectOfType<gamemanage>().canvass);
                FindObjectOfType<gamemanage>().gameover();
                Destroy(gameObject);
            }
        }
    }
}

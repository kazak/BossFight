
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour 
{   
    public float health = 5000;
    
    public float maxhealth = 5000;
    public GameObject mutantMesh;
    public Texture[] texture = new Texture[2];
    private int texRef;
    private Animator anim;

    public Slider slider;
    public Text text;
    public float StunnedReload;
    private bool Stunned = false;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Update()
    {
        Debug.Log(Stunned);
        Debug.Log(StunnedReload);
        WeakSpotEnable();
        WeakSpotEnableRight();
        text.text = $"{health}/{maxhealth}";
        slider.maxValue = maxhealth;
        slider.value = health;
        
        //Debug.Log(health);
        
        if (health <= 0)
        {   
            health = 0; 
            anim.SetBool("isDead", true);
            
        }
        
    }

    public void FixedUpdate()
    {
        if (StunnedReload > 0)
        {
            StunnedReload -= Time.deltaTime;
            Stunned = false;
            anim.SetBool("Stunned", false);

        }
        //если хотим стан
        else if (StunnedReload <= 0.01f)
        {
            StunnedReload = 0.3f;  //интервал стана
            Stunned = true;
            
            anim.SetBool("Stunned", true);

        }
    }



    public void ReceiveCollision(ref Collision col, ref string name)
    {
        if (col.transform.tag == "bullet")
        {
            health -= 250;
            Destroy(col.gameObject);
            if (mutantMesh != null && health > 0)
            {
                if (name == "LeftHit")
                {
                    StartCoroutine(HitFlash(0));
                }
                if (name == "RightHit")
                {
                    StartCoroutine(HitFlash(1));
                }
            }
        }
    }

    public void WeakSpotEnable()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mutantMesh != null)
            {
                StartCoroutine(WeakSpots(1));
                
            }
        }
    }
    public void WeakSpotEnableRight()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (mutantMesh != null)
            {
                StartCoroutine(WeakSpots(0));
                
            }
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        
        
        if (other.gameObject.CompareTag("bullet"))
        {
            health -= 1;
            Destroy(other.gameObject);
        }
    }


    private IEnumerator HitFlash(int num)
    {
        for (int i = 0; i < 5; i++)
        {
            mutantMesh.GetComponent<Renderer>().material.SetTexture("_EmissionMap", texture[num]);
            mutantMesh.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(0.1f);
            mutantMesh.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    private IEnumerator WeakSpots(int num)
    {
        for (int i = 0; i < 5; i++)
        {
        mutantMesh.GetComponent<Renderer>().material.SetTexture("_EmissionMap", texture[num]);
        mutantMesh.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.1f);
        mutantMesh.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    private IEnumerator WeakSpotsRight(int num)
    {
        for (int i = 0; i < 5; i++)
        {
            mutantMesh.GetComponent<Renderer>().material.SetTexture("_EmissionMap", texture[num]);
            mutantMesh.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(0.1f);
            mutantMesh.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}

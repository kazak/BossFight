
using UnityEngine;
using UnityEngine.UI;

public class MissileController : MonoBehaviour
{
    private Animator anim;
    public Transform target;
    public GameObject explosive;
    public GameObject exhaust;
    public Image damageImage;
    public Color damageColor;
    float colorSmoothing = 6f;
    public enum Missile_state
    {
        idle,
        start,
        fly,
        end
    }
    public Missile_state missile_state_t;

    float timer = 0;
    public float speed_move;
    [SerializeField]
    public float speed_rotate;
    
   
    Vector3 start_pos;
    Renderer bools;
    private float damageAmount = 10f;

    void Start()
    {
        exhaust.SetActive(false);
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        start_pos = gameObject.transform.position;
    }
   void Update()
    {
       
        bools = GetComponent<MeshRenderer>();
        bools.enabled = false;

       
        switch (missile_state_t)
            {
                case Missile_state.idle:

                    if (timer <= 2) { timer += Time.deltaTime; }
                    if (timer > 2) { 
                    missile_state_t = Missile_state.start; 
                    Instantiate(explosive, gameObject.transform.position, Quaternion.identity);
                    damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
                }
                    
                   break;
                case Missile_state.start:
                    bools.enabled = true;
                    float start_dist = Vector3.Distance(gameObject.transform.position, start_pos);
                    gameObject.transform.Translate(Vector3.up * speed_move * Time.deltaTime);
                        
                if (start_dist >= 5)
                    {
                    exhaust.SetActive(true);
                        missile_state_t = Missile_state.fly;
                    }
                 
                    
                    break;

                case Missile_state.fly:
                    bools.enabled = true;
                    gameObject.transform.Translate(Vector3.up * speed_move * Time.deltaTime);
                    Vector3 target_vector = target.transform.position - gameObject.transform.position;
                    gameObject.transform.up = Vector3.Slerp(gameObject.transform.up, target_vector, speed_rotate * Time.deltaTime);

                    if (target_vector.magnitude < 1)
                    {
                        missile_state_t = Missile_state.end;
                    }

                    break;

                case Missile_state.end:
                    Instantiate(explosive, gameObject.transform.position, Quaternion.identity);
                    bools.enabled = true;
                     PlayerHealth.singleton.PlayerDamage(damageAmount);
                     damageImage.color = damageColor;
                    Destroy(gameObject);
                    
                    break;
            }
        }
        
    }


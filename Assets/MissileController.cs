using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target;
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
    
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        start_pos = gameObject.transform.position;
    }
    private void Update()
    {
        switch (missile_state_t)
        {
            case Missile_state.idle:
                if (timer <= 2) { timer += Time.deltaTime; }
                if (timer > 2) { missile_state_t = Missile_state.start; }
                break;
            case Missile_state.start:
                float start_dist = Vector3.Distance(gameObject.transform.position, start_pos);
                gameObject.transform.Translate(Vector3.up * speed_move * Time.deltaTime);
                if (start_dist >= 5)
                {
                    missile_state_t = Missile_state.fly;
                }
                break;
            
            case Missile_state.fly:
                gameObject.transform.Translate(Vector3.up* speed_move *Time.deltaTime);
                Vector3 target_vector = target.transform.position - gameObject.transform.position;
                gameObject.transform.up = Vector3.Slerp(gameObject.transform.up, target_vector, speed_rotate * Time.deltaTime);
                
                if (target_vector.magnitude < 1 )
                {
                    missile_state_t = Missile_state.end;
                }

                break;
            
            case Missile_state.end:
                Destroy(gameObject);
                
                break;
        }
    }
}

using UnityEngine;

public class BossController : MonoBehaviour
{
    public int attackRange = 3;
    private Transform player;
    private Animator anim;
    private int attackType;
    private bool setForce = false;
    private Vector3 direction;
    private bool Stunned = false;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        
    }


    private void Update()
    {
        
        if (!anim.GetBool("isDead"))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            //Find the direction
            direction = player.position - transform.position;
            //If the boss is far enough away from the player, rotate to look at the player.
            if (direction.magnitude > 2f)
            {
                transform.LookAt(new Vector3(player.position.x, 0, player.position.z));
            }
            //Set a random value between a range we set to choose our attack type.
           
            if (distance < 5f )
            {   
                attackType = 3;
                // attackType = Random.Range(1, attackRange + 1);
                
            }
            if (distance > 5 && distance <20 )
            {   //Jump Attack
                attackType = 2;
                anim.SetInteger("isAttacking", 2);
                Stunned = true;
                
            }
            if (Stunned == true)
            {   //Stunned after JumpAttack
                anim.SetBool("isStunned", true);
                Stunned = false;
            }
        }

        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //If the boss is too far from the player, run towards them.
        if (!anim.GetBool("isDead"))
        {
            
            if (direction.magnitude > 3f)
            {
                anim.SetBool("isRunning", true);
                attackType = 0;
                anim.SetInteger("isAttacking", 0);
            }
            //If we are close enough, do an attack.
            else
            {
                anim.SetBool("isRunning", false);
                anim.SetInteger("isAttacking", attackType);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.1f)
                {
                    setForce = true;
                }
                //At the moment we trigger damage at force when animation is about 50% for all attacks.
                if (setForce && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
                {
                    //Add some knock back to the player.
                    player.gameObject.GetComponent<Rigidbody>().AddExplosionForce(5.0f, transform.position, 6.0f, 4.0f, ForceMode.Impulse);
                    if (direction.magnitude <= 3f)
                    {
                        //We can replace this with something that sends damage to the player.
                        Debug.Log("50%");
                    }
                    setForce = false;
                }
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetInteger("isAttacking", 0);
            
        }
    }
    void AttackPlayer()
    {
        anim.SetInteger("isAttacking", 1);
    }
}

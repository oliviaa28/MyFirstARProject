using UnityEngine;
using Vuforia;

public class CactusProximity : MonoBehaviour
{
    public ObserverBehaviour targetImage1, targetImage2;
    public Animator cactusAnimator1, cactusAnimator2;

    public float attackDistance = 0.08f;
    public float movingSensor = 0.004f;
    private Vector3 previousPosition1, previousPosition2;
    public int attacker = 0; // 1-> cactus 1 2-> cactus2\

    private float attackTime = 0 , deadTime = 0;
    public float attackDuration = 2f, deadDuration =3f; //durata animatiilor 

    public bool isDead = false;


    // Update is called once per frame
    void Update()
    {
        float distance, movement1, movement2;
        bool isMoving1 = false;
        bool isMoving2 = false;
        bool isSeen1 = false, isSeen2 = false;

        // how much the target images moved 
        movement1 = Vector3.Distance(targetImage1.transform.position, previousPosition1);
        movement2 = Vector3.Distance(targetImage2.transform.position, previousPosition2);


        if( movement1> movingSensor)
            isMoving1 = true;

        if (movement2 > movingSensor)
            isMoving2 = true ;

        cactusAnimator1.SetBool("isMoving", isMoving1);
        cactusAnimator2.SetBool("isMoving", isMoving2);

      //  Debug.Log("M1: " + movement1 + "  M2: " + movement2);

        if( targetImage2.TargetStatus.Status == Status.TRACKED || targetImage2.TargetStatus.Status == Status.EXTENDED_TRACKED)
            isSeen2 = true;

        if (targetImage1.TargetStatus.Status == Status.TRACKED || targetImage1.TargetStatus.Status == Status.EXTENDED_TRACKED)
            isSeen1 = true; 


        if (isSeen1 && isSeen2) // if we can see the taerget images
        {
            distance = Vector3.Distance(targetImage1.transform.position,
                                         targetImage2.transform.position);

            if (distance <= attackDistance) // if the targets are close
            {

                if (attacker == 0 && isDead == false )
                {
                    if (isMoving2)
                    {
                        attacker = 2;
                        Debug.Log("Atacator 2! distanta = " + distance + ", miscare2 = " + movement2);
                    }
                    else if (isMoving1)
                    {
                        attacker = 1;
                        Debug.Log("Atacator 1! distanta = " + distance + ", miscare1 = " + movement1);
                    }
                }
            }
            else
                attacker = 0;

            if (Time.frameCount % 60 == 0)
                Debug.Log("Distanta = " + distance + "  AttackDistance = " + attackDistance);
               }

        else { // if we do not see a target 
            attacker = 0;
        }


        if (Time.frameCount % 60 == 0)
            Debug.Log("Seen1: " + isSeen1 + " Seen2: " + isSeen2 + " Attacker: " + attacker);

        if (attacker != 0)
            attackTime += Time.deltaTime;
        else
            attackTime = 0;

        if(attackTime >= attackDuration)
        {
            cactusAnimator1.SetBool("isDead", attacker == 2); 
            cactusAnimator2.SetBool("isDead", attacker == 1);
            attackTime = 0;
            attacker = 0;
            isDead = true;
        }

        if (isDead)
        {
            deadTime += Time.deltaTime;

            if (deadTime >= deadDuration)
            {
                cactusAnimator1.SetBool("isDead", false);
                cactusAnimator2.SetBool("isDead", false);
                deadTime = 0;
               isDead = false;
           }
        }

        previousPosition1 = targetImage1.transform.position;
        previousPosition2 = targetImage2.transform.position;

        cactusAnimator1.SetBool("isAttacking", attacker == 1); 
        cactusAnimator1.SetBool("isHit", attacker == 2);

        cactusAnimator2.SetBool("isAttacking", attacker == 2);
        cactusAnimator2.SetBool("isHit", attacker == 1);

        

    }
}

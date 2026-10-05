using UnityEngine;
using Vuforia;

public class CactusProximity : MonoBehaviour
{
    public ObserverBehaviour targetImage1, targetImage2;
    public Animator cactusAnimator1, cactusAnimator2;

    public float attackDistance = 0.20f;
    public float movingSensor = 0.005f;
    private Vector3 previousPosition1, previousPosition2;

    void Update()
    {
        float distance, movement1, movement2;
        bool isAttacking = false;
        bool isMoving1 = false;
        bool isMoving2 = false;
        
        // how much the target images moved 
        movement1 = Vector3.Distance(targetImage1.transform.position, previousPosition1);
        movement2 = Vector3.Distance(targetImage2.transform.position, previousPosition2);

        if( movement1> movingSensor)
            isMoving1 = true;

        if (movement2 > movingSensor)
            isMoving2 = true ;

        cactusAnimator1.SetBool("isMoving", isMoving1);
        cactusAnimator2.SetBool("isMoving", isMoving2);

        if (targetImage1.TargetStatus.Status == Status.TRACKED && targetImage2.TargetStatus.Status == Status.TRACKED)
        {
            distance = Vector3.Distance(targetImage1.transform.position,
                                         targetImage2.transform.position);

            if (distance <= attackDistance)
                isAttacking = true;
        }

        previousPosition1 = targetImage1.transform.position;
        previousPosition2 = targetImage2.transform.position;

        cactusAnimator1.SetBool("isAttacking", isAttacking);
        cactusAnimator2.SetBool("isAttacking", isAttacking);
    }
}

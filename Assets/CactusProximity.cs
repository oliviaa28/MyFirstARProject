using UnityEngine;
using Vuforia;

public class CactusProximity : MonoBehaviour
{
    public ObserverBehaviour targetImage1, targetImage2;
    public Animator cactusAnimator1, cactusAnimator2;
    public float attackDistance= 0.20f;

    // Update is called once per frame
    void Update()
    {
        float distance;
        bool isAttacking = false;

        if (targetImage1.TargetStatus.Status == Status.TRACKED && targetImage2.TargetStatus.Status == Status.TRACKED)
        {
            distance = Vector3.Distance(targetImage1.transform.position,
                                         targetImage2.transform.position);


            if (distance <= attackDistance)
                isAttacking = true;

        }

        cactusAnimator1.SetBool("isAttacking", isAttacking);
        cactusAnimator2.SetBool("isAttacking", isAttacking);
    }
}

using UnityEngine;

public class CactusProximity : MonoBehaviour
{
    public Transform targetImage1, targetImage2;
    public Animator cactusAnimator1, cactusAnimator2;
    public float attackDistance= 0.20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance;

        distance = Vector3.Distance( targetImage1.position, targetImage2.position);
        Debug.Log("Distanta: " + distance);

        if (distance <= attackDistance)
        {
            cactusAnimator1.SetBool("isAttacking", true);
            cactusAnimator2.SetBool("isAttacking", true);
        }
        else {
            cactusAnimator1.SetBool("isAttacking", false);
            cactusAnimator2.SetBool("isAttacking", false);
        }


    }
}

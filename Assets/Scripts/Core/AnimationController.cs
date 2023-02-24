using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Transform targetObject;
    public Animator animator;

    public float distanceThreshold;
    public string attack;
    public string walk;

    private void Update()
    {



        float distance = Vector3.Distance(transform.position, targetObject.position);


        if (distance <= distanceThreshold)
        {
            animator.Play("attack");
        }
        else
        {
            animator.Play("walk");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
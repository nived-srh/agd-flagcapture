using UnityEngine;

public class AnimationControllerPlant : MonoBehaviour
{
    public Transform targetObject;
    public Animator animator;

    public float distanceThreshold;
    public string attackRight, attackLeft;
    public string walk;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);
        float distanceDirection = transform.position.x - targetObject.position.x;

        if (distanceDirection < 0 && distance <= distanceThreshold)
        {
            animator.Play(attackRight);
        }
        else if (distanceDirection > 0 && distance <= distanceThreshold)
        {
            animator.Play(attackLeft);
        }
        else
        {
            animator.Play(walk);
        }
    }
}
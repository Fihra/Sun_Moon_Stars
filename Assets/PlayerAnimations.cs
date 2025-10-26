using UnityEngine;


public class PlayerAnimations : MonoBehaviour
{
    public InputHandler otherInputHandler; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         otherInputHandler = GetComponent<InputHandler>();
    }

    [SerializeField] protected Animator thisAnimator;
    protected Vector3 oldPos = Vector3.zero;
    protected Vector3 deltaPos = Vector3.zero;

    public void SetWalking(bool val)
    {
        thisAnimator.SetBool("isWalking", val);
    }

    protected void DeltaMovement()
    {
        deltaPos = transform.position - oldPos;

        if (deltaPos.sqrMagnitude > .001f * Time.deltaTime)
            SetWalking(true);
        else
            SetWalking(false);

        oldPos = transform.position;
    }
}

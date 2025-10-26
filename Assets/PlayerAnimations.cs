using UnityEngine;


public class PlayerAnimations : MonoBehaviour
{
    public Animator thisAnimator;
    InputHandler getMovement; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisAnimator = transform.Find("HumanMale_Character_Free").GetComponent<Animator>();
        getMovement = GetComponent<InputHandler>();
    }

    private void Update()
    {
        if(thisAnimator != null)
        {
            if(getMovement.GetInputValues() != Vector2.zero)
            {
                thisAnimator.SetBool("isMoving", true);
            } else
            {
                thisAnimator.SetBool("isMoving", false);
            }
        }
    }
}

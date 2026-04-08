using UnityEngine;

public class pAnimation : MonoBehaviour
{
    
    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
    public void StopAnimation()
    {
        animator.StopPlayback();
    }
    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    public void SetBool(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }
    
}

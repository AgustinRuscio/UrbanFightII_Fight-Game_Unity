//--------------------------------------------
//          Agustin Ruscio & Merdeces Riego
//--------------------------------------------


using Fusion;

public class PlayerView 
{
    private NetworkMecanimAnimator _animator;

    public PlayerView(NetworkMecanimAnimator animator)
    {
        _animator = animator;
    }

    public void Move(float x) => _animator.Animator.SetFloat("Move", x);
    public void Jump() => _animator.SetTrigger("Jump");

    public void Crouch (bool isCrouching) => _animator.Animator.SetBool("Crouch", isCrouching);

    public void Punch() => _animator.SetTrigger("Punch");
    public void GetHurt() => _animator.SetTrigger("GetHurt");
    public void LowKick() => _animator.SetTrigger("LowKick");
    public void HighKick() => _animator.SetTrigger("HighKick");
    public void Blocking(bool isBlocking) => _animator.Animator.SetBool("Blocking", isBlocking);
}
using System;
using UnityEngine;

public class PlayerView 
{
    private Animator _animator;

    public PlayerView(Animator animator)
    {
        _animator = animator;
    }

    public void Move(float x) => _animator.SetFloat("Move", x);
    public void Jump() => _animator.SetTrigger("Jump");

    public void Crouch (bool isCrouching) => _animator.SetBool("Crouch", isCrouching);

    public void Punch() => _animator.SetTrigger("Punch");
    public void GetHurt() => _animator.SetTrigger("GetHurt");
    public void LowKick() => _animator.SetTrigger("LowKick");
    public void HighKick() => _animator.SetTrigger("HighKick");
}
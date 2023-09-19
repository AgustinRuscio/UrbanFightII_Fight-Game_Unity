using UnityEngine;
using System;

public class PlayerController
{
    private PlayerModel _model;

    public  Action OnUpdate;
    public  Action OnFixedUpdate;

    public PlayerController(PlayerModel model)
    {
        _model = model;

        SetAction();
    }

    private void SetAction()
    {
        OnFixedUpdate += MovementController;

        OnUpdate += Punch;
        OnUpdate += Crouch;
        OnUpdate += HighKick;
        OnUpdate += LowKick;
        OnUpdate += JumpInput;
    }

    public void MovementController() => _model.Move(Input.GetAxis("Horizontal"));

    public void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) _model.Jump();
    }

    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C)) _model.Crouch();
    }

    public void Punch()
    {
        if (Input.GetKeyDown(KeyCode.J)) _model.Punch();
    }

    public void HighKick()
    {
        if (Input.GetKeyDown(KeyCode.I)) _model.HighKick();
    }

    public void LowKick()
    {
        if (Input.GetKeyDown(KeyCode.K)) _model.LowKick();
    }
}

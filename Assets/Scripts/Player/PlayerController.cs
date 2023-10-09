//--------------------------------------------
//          Agustin Ruscio & Merdeces Riego
//--------------------------------------------


using UnityEngine;
using System;

public class PlayerController
{
    private PlayerModel _model;

    public Action OnUpdate;
    public Action OnFixedUpdate;

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
        OnUpdate += Block;
    }

    public void MovementController() => _model.Move(Input.GetAxis("Horizontal"));

    public void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) _model.Jump();
    }

    public void Crouch() => _model.Crouch(Input.GetKey(KeyCode.S));
    
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

    public void Block() => _model.Blocking(Input.GetKey(KeyCode.Q)); 
}
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public float xMovement;
    public NetworkBool isJump; 
    public NetworkBool isCrouching; 
    public NetworkBool isLanded; 
    public NetworkBool isBlocking; 
    public NetworkBool _canMove; 
}

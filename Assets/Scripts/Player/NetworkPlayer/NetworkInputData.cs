//--------------------------------------------
//          Agustin Ruscio & Merdeces Riego
//--------------------------------------------


using Fusion;

public struct NetworkInputData : INetworkInput
{
    public float xMovement;
    public NetworkBool isJump; 
    public NetworkBool isCrouching; 
    public NetworkBool isBlocking; 
    
    public NetworkBool _punch; 
    public NetworkBool _highKick; 
    public NetworkBool _lowKick; 
}
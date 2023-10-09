//--------------------------------------------
//          Agustin Ruscio & Merdeces Riego
//--------------------------------------------


using Fusion;

public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local { get; private set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
            Local = this;
    }
}
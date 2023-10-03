using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerInput : MonoBehaviour
{
    NetworkInputData _inputData;

    bool isJumpPessed;
    bool isCrouching;
    bool isBlocking;
    
    bool punch;
    bool hKich;
    bool lKick;

    void Start() => _inputData = new();
    

    void Update()
    {
        _inputData.xMovement = Input.GetAxis("Horizontal");
        _inputData.isBlocking = Input.GetKey(KeyCode.Q);
        _inputData.isCrouching = Input.GetKey(KeyCode.S);

        if (Input.GetKeyDown(KeyCode.W))
            isJumpPessed = true;
        
        

        if (Input.GetKeyDown(KeyCode.J))
            punch = true;

        if (Input.GetKeyDown(KeyCode.I))
            hKich = true;

        if (Input.GetKeyDown(KeyCode.K))
            lKick = true;


    }

    public NetworkInputData GetInputData()
    {
        _inputData.isJump = isJumpPessed;
        isJumpPessed = false;
        
        _inputData._punch = punch;
        punch = false;
        
        _inputData._highKick = hKich;
        hKich = false;
        
        _inputData._lowKick = lKick;
        lKick = false;

        return _inputData;
    }
}

using System.Collections;
using UnityEngine;
using System;
using Fusion;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : NetworkBehaviour, IDamageable
{
    //NetWork

    NetworkInputData inputData;



    [Header("Components")]
    private NetworkRigidbody _rigidBody;

    [SerializeField]
    private Animator _animator;

    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private LayerMask _groundMask;

    [Header("Atributes")]
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private float _maxLlife;

    [Networked(OnChanged = nameof(OnLifeChange))] [HideInInspector]
    public float _life { get; set; }

    public event Action<float> OnDamage = delegate { };

    [SerializeField]
    private Transform target;

    private Vector3 _direction;

    #region Delegates MVC

    //-----Controller
    private Action OnControllerUpdate;
    private Action OnControllerFixedUpdate;

    //-----View
    public event Action OnPunchAnim;
    public event Action OnLowKickAnim;
    public event Action OnHighKickAnim;
    public event Action OnJumpAnim;
    public event Action OnGetHurtnim;

    public event Action<bool> OnCrouchAnim;
    public event Action<bool> OnBlocking;
    public event Action<float> OnMoveAnim;
    #endregion


    [Header("Attack Zones")]
    [SerializeField]
    private GameObject _midPunchZone;

    [SerializeField]
    private GameObject _downPunchZone;

    [SerializeField]
    private GameObject _upPunchZone;

    [Header("States")]
    private bool _canMove = true;
    private bool _crouching = false;
    private bool _blocking = false;

    void Awake()
    {
        _rigidBody = GetComponent<NetworkRigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        //Setting controller Methods in Actions
        var controller = new PlayerController(this);

        OnControllerUpdate += controller.OnUpdate;
        OnControllerFixedUpdate += controller.OnFixedUpdate;

        //Setting view Methods on Actions
        var view = new PlayerView(_animator);
        _life = _maxLlife;

        OnMoveAnim += view.Move;
        OnCrouchAnim += view.Crouch;
        OnJumpAnim += view.Jump;
        OnPunchAnim += view.Punch; 
        OnLowKickAnim += view.LowKick;
        OnHighKickAnim += view.HighKick;
        OnGetHurtnim += view.GetHurt;
        OnBlocking += view.Blocking;
    }


    void FixedUpdate()
    {
        OnControllerFixedUpdate();
    }

    private void Update()
    {
        OnControllerUpdate();
        //transform.LookAt(target.position);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out inputData))
        {
            if (inputData.isJump)
                Jump();

            Move(inputData.xMovement);
        }
    }

    private static void OnLifeChange(Changed<PlayerModel> changed)
    {
        var behaviour = changed.Behaviour;
    
        behaviour.OnDamage(behaviour._life / behaviour._maxLlife);
    }

    public void Move(float xMovement)
    {
        if (!_canMove || _crouching || !isLanded || _blocking) return;

        _direction = new Vector3(xMovement, 0, 0);

        _direction *= Time.deltaTime;
        _direction.Normalize();
        _direction *= _speed;

        OnMoveAnim(_direction.x);
        _rigidBody.Rigidbody.AddForce(_direction);
    }

    public void Jump()
    {
        if (!_canMove || _crouching || !isLanded|| _blocking) return;

        _rigidBody.Rigidbody.AddForce(_rigidBody.Rigidbody.velocity.x, _jumpForce, _rigidBody.Rigidbody.velocity.z);
        OnJumpAnim();
    }

    public void TakeDamage(float dmg) => RPC_GetHit(dmg);

    [Rpc(RpcSources.Proxies, RpcTargets.StateAuthority)]
    public void RPC_GetHit(float dmg)
    {
        if (_blocking) return;

        _life -= dmg;

        if (_life <= 0)
            Died();

        OnGetHurtnim();
    }

    public void Blocking(bool isBloking)
    {
        _blocking = isBloking;
        OnBlocking(_blocking);
    }

    public void Punch()
    {
        if (!_canMove || _crouching || !isLanded || _blocking) return;

        OnAttackiong(false);
        OnPunchAnim();
        _midPunchZone.SetActive(true);
        StartCoroutine(Deactivate(_midPunchZone, .7f));
    }

    public void HighKick()
    {
        if (!_canMove || _crouching || !isLanded || _blocking) return;

        OnAttackiong(false);
        OnHighKickAnim();
        _upPunchZone.SetActive(true);
        StartCoroutine(Deactivate(_upPunchZone, 1.5f));
    }

    public void LowKick()
    {
        if (!_canMove || _crouching || !isLanded || _blocking) return;

        OnAttackiong(false);
        OnLowKickAnim();
        _downPunchZone.SetActive(true);
        StartCoroutine(Deactivate(_downPunchZone, 2f));
    }

    private void OnAttackiong(bool state)
    {
        _canMove = state;
    }

    public void Crouch(bool isBool)
    {
        if (!isLanded || _blocking) return;

        _crouching = isBool;

        OnCrouchAnim(_crouching);

        if (_crouching)
        {
            _capsuleCollider.center = new Vector3(0, -0.33f, 0.33f);
            _capsuleCollider.height = 1.33f;
        }
        else
        {
            _capsuleCollider.center = new Vector3(0, -0.1f, 0);
            _capsuleCollider.height = 1.79f;
        }
    }


    IEnumerator Deactivate(GameObject obj, float cd)
    {
        yield return new WaitForSeconds(cd);
        obj.SetActive(false);
        OnAttackiong(true);
    }

    private bool isLanded{ get { return Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Vector3.down, 1.5f, _groundMask); } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 dir = Vector3.down * 1.5f;

        Gizmos.DrawRay(transform.position + new Vector3(0, 0.2f, 0), dir);
    }

    private void Died()
    {
        Runner.Shutdown();
    }
}
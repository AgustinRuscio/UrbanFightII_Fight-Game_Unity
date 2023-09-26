using UnityEngine;
using Fusion;


public class PunchZone : NetworkBehaviour
{
    [SerializeField]
    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (!Object || !Object.HasStateAuthority) return;

        var damageable = other.GetComponent<IDamageable>();

        if(damageable == null) return;

        damageable.TakeDamage(_damage);
    }
}
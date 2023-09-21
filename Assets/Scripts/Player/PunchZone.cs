using UnityEngine;

public class PunchZone : MonoBehaviour
{
    [SerializeField]
    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if(damageable == null) return;

        damageable.TakeDamage(_damage);
    }
}
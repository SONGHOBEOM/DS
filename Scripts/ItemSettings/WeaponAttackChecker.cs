using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WeaponAttackChecker : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    private bool onHit;
    private void OnEnable()
    {
        onHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            if(onHit == false)
            {
                onHit = true;
                var damage = Calculator.CalculateWeaponDamage(weaponData);

                var monster = other.GetComponent<Monster>();
                monster.GetDamaged(damage);

                var collisionPoint = monster.GetOnHitEffectPoint().transform;
                StartCoroutine(ShowEffect(collisionPoint));

                SoundManager.Instance.PlayClip("OnHit");
            }
        }
    }

    private IEnumerator ShowEffect(Transform point)
    {
        var effect = ResourceManager.Instance.Instantiate("CollisionEffect", point);
        effect.transform.localPosition = Vector3.zero;

        yield return YieldCache.waifForSecond;

        ResourceManager.Instance.Destroy(effect);
    }
}

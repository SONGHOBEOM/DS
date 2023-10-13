using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private SkillData skillData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            var damage = Calculator.CalculateSkillDamage(skillData);

            var monster = other.GetComponent<Monster>();
            monster.GetDamaged(damage);

            var collisionPoint = monster.GetOnHitEffectPoint().transform;
            StartCoroutine(ShowEffect(collisionPoint));

            SoundManager.Instance.PlayClip("OnSkillHit");
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

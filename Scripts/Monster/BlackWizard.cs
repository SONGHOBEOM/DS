using System.Collections;
using UnityEngine;

public class BlackWizard : Monster
{
    [SerializeField] private Transform castingPoint;
    protected override void OnEnable()
    {
        this.monsterType = Define.ObjectType.BlackWizard;
        if (monsterData == null)
            this.monsterData = ObjectHelper.GetData(this.monsterType) as MonsterData;
        InitInfo();
        base.OnEnable();
    }

    protected override IEnumerator Attack()
    {
        var attackDelay = monsterData.attackDelay;
        while (true)
        {
            if (isDead == false)
            {
                var target = EntityManager.Instance.player.transform;
                transform.LookAt(target);
                isAttacking = true;

                StartCoroutine(SpawnFireball(target));

                yield return YieldCache.GetCachedTimeInterval(attackDelay);

                isAttacking = false;

                if (Vector3.Distance(target.position, gameObject.transform.position) > navMeshAgent.stoppingDistance)
                {
                    ChangeState(MonsterState.Idle);
                    yield break;
                }

            }
            else
                yield break;
        }
    }

    public IEnumerator SpawnFireball(Transform target)
    {

        var newFireball = ResourceManager.Instance.Instantiate<MonsterAttackChecker>("FireBall", castingPoint);
        InitFireBall();

        animator.SetTrigger("Attack");

        var targetCurPosition = target.transform.position;


        Vector3 initPosition = newFireball.transform.position;

        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * 0.5f;
            var rotation = Random.insideUnitSphere * Time.deltaTime;
            newFireball.transform.Rotate(rotation, Space.Self);
            newFireball.transform.position = Vector3.Lerp(initPosition, targetCurPosition + Vector3.up, i);
            yield return 0;
        }
        yield return YieldCache.waitForEndOfFrame;
        ResourceManager.Instance.Destroy(newFireball.gameObject);

        void InitFireBall()
        {
            newFireball.transform.localPosition = Vector3.zero;
            newFireball.transform.localScale = Vector3.one;
            newFireball.transform.SetParent(null);
            newFireball.SetMonsterData(monsterData);
            newFireball.SetMonsterType(monsterType);
        }
    }
}

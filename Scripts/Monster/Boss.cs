using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : Monster
{
    [SerializeField] private BossSkill[] skills;
    private PatternInfo patternInfo;
    private int patternNum = 0;
    protected override void OnEnable()
    {
        this.monsterType = Define.ObjectType.Boss;
        if(monsterData == null)
            this.monsterData = ObjectHelper.GetData(this.monsterType) as MonsterData;

        InitInfo();
        base.OnEnable();


        foreach (var monsterAttackChecker in monsterAttackCheckers)
        {
            monsterAttackChecker.SetMonsterData(this.monsterData);
            monsterAttackChecker.SetMonsterType(this.monsterType);
        }
        SoundManager.Instance.PlayClip(monsterData.monsterName, transform);
    }
    protected override IEnumerator Chase()
    {
        while (true)
        {
            if (isDead == false)
            {
                animator.SetBool("Detect", true);
                var target = EntityManager.Instance.player.transform.position;

                transform.LookAt(target);
                navMeshAgent.SetDestination(target);
                if (playerChecker.isDetecting == false)
                {
                    animator.SetBool("Detect", false);
                    ChangeState(MonsterState.Idle);
                }
                if (Vector3.Distance(target, gameObject.transform.position) < navMeshAgent.stoppingDistance)
                {
                    yield return YieldCache.GetCachedTimeInterval(monsterData.onTriggerTime);
                    animator.SetBool("Detect", false);
                    ChangeState(MonsterState.Attack);
                }
                yield return YieldCache.waitForEndOfFrame;
            }
            else
                yield break;
        }
    }

    protected override IEnumerator Attack()
    {
        if (patternInfo == null)
        {
            this.patternInfo = DataManager.Instance.entireDataContainer.patternDataContainer.Get(name);
        }
        while (true)
        {
            if(isDead == false)
            {
                var pattern = patternInfo.attackPatterns[patternNum];

                var skill = skills.FirstOrDefault(x => x.name == pattern);
                skill.SetPatternInfo(patternInfo);

                var target = EntityManager.Instance.player.transform;
                transform.LookAt(target);
                animator.SetTrigger(pattern);

                yield return YieldCache.GetCachedTimeInterval(patternInfo.playDelay);

                skill.gameObject.SetActive(true);
                patternNum++;

                yield return YieldCache.GetCachedTimeInterval(patternInfo.playDuration);

                skill.gameObject.SetActive(false);

                yield return YieldCache.GetCachedTimeInterval(monsterData.attackDelay);


                if (Vector3.Distance(target.position, gameObject.transform.position) > navMeshAgent.stoppingDistance)
                    ChangeState(MonsterState.Chase);
            }
        }
    }

    protected override IEnumerator Die()
    {
        UIManager.Instance.OpenUI<ClearUI>();
        WaveTimer.clear?.Invoke();
        SoundManager.Instance.PlayClip($"{monsterData.monsterName}Die");
        return base.Die();
    }

}

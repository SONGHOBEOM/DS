using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState { Idle, Chase, Attack, Die }
public class Monster : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected PlayerChecker playerChecker;
    [SerializeField] protected MonsterAttackChecker[] monsterAttackCheckers;
    [SerializeField] protected CapsuleCollider monsterCollider;
    [SerializeField] protected Rigidbody rigidBody;
    [SerializeField] protected MonsterHealthBar healthBar;
    [SerializeField] protected GameObject onHitEffectPoint;

    public static bool dieFlag = false;
    protected MonsterData monsterData;
    protected Define.ObjectType monsterType;

    // Monster FSM //
    protected Dictionary<MonsterState, Func<IEnumerator>> monsterStates = new Dictionary<MonsterState, Func<IEnumerator>>();
    protected Coroutine coroutine;
    protected MonsterState monsterState;
    public NavMeshAgent navMeshAgent;

    protected float monsterArmor;
    protected float monsterDamage;
    protected float monsterHealth;
    protected float monsterSpeed;
    protected float detectRange;
    protected float attackDelay;
    protected float expvValue;
    protected bool isDead = false;
    protected bool isAttacking = false;

    protected float destroyDelayTime = 3.0f;


    protected virtual void OnEnable()
    {
        if (monsterStates.Count == 0)
        {
            monsterStates.Add(MonsterState.Idle, Idle);
            monsterStates.Add(MonsterState.Chase, Chase);
            monsterStates.Add(MonsterState.Attack, Attack);
            monsterStates.Add(MonsterState.Die, Die);
        }
        StartCoroutine(CheckIsDead());
        ChangeState(MonsterState.Idle);
    }
    protected void ChangeState(MonsterState newState)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        monsterState = newState;

        if (coroutine == null)
            coroutine = StartCoroutine(monsterStates[newState].Invoke());
    }


    protected void InitInfo()
    {
        this.monsterArmor = monsterData.monsterArmor;
        this.monsterDamage = monsterData.monsterDamage;
        this.monsterHealth = monsterData.monsterHealth;
        this.attackDelay = monsterData.attackDelay;
        this.expvValue = monsterData.expValue;
        this.isDead = false;
        monsterCollider.enabled = true;
        rigidBody.isKinematic = false;
        healthBar.SetHealthBar(this.monsterHealth);
    }

    protected IEnumerator Idle()
    {
        while (true)
        {
            if (isDead == false)
            {
                if (playerChecker.isDetecting)
                {
                    if(isAttacking == false)
                        ChangeState(MonsterState.Chase);
                }
                yield return YieldCache.waitForEndOfFrame;
            }
            else
                yield break;
        }
        
    }
    protected virtual IEnumerator Chase()
    {
        while (true)
        {
            if (isDead == false)
            {
                if(isAttacking)
                {
                    yield return YieldCache.waitForEndOfFrame;
                    continue;
                }

                animator.SetBool("Detect", true);
                var target = EntityManager.Instance.player.transform.position;

                transform.LookAt(target);
                navMeshAgent.SetDestination(target);
                if (playerChecker.isDetecting == false)
                {
                    animator.SetBool("Detect", false);
                    ChangeState(MonsterState.Idle);
                    yield break;
                }
                if (Vector3.Distance(target, gameObject.transform.position) <= navMeshAgent.stoppingDistance)
                {
                    if (isAttacking == false)
                    {
                        ChangeState(MonsterState.Attack);
                        animator.SetBool("Detect", false);
                        yield break;
                    }
                    else
                        yield return YieldCache.waitForEndOfFrame;
                }
                yield return YieldCache.waitForEndOfFrame;
            }
            else
                yield break;
        }
    }
    protected virtual IEnumerator Attack()
    {
        SetMonsterAttackCheckerInfo();
        var onTriggerTime = monsterData.onTriggerTime;
        var offTriggerTime = monsterData.offTriggerTime;
        var attackDelay = monsterData.attackDelay;
        while(true)
        {
            if (isDead == false)
            {
                var target = EntityManager.Instance.player.transform;
                transform.LookAt(target);

                isAttacking = true;
                animator.SetTrigger("Attack");
                yield return YieldCache.GetCachedTimeInterval(onTriggerTime);
                monsterAttackCheckers[0].gameObject.SetActive(true);

                yield return YieldCache.GetCachedTimeInterval(offTriggerTime);
                monsterAttackCheckers[0].gameObject.SetActive(false);

                isAttacking = false;
                if (Vector3.Distance(target.position, gameObject.transform.position) > navMeshAgent.stoppingDistance)
                {
                    monsterAttackCheckers[0].gameObject.SetActive(false);
                    ChangeState(MonsterState.Idle);
                    yield break;
                }
                yield return YieldCache.GetCachedTimeInterval(attackDelay);
            }
            else
                yield break;
        }

        void SetMonsterAttackCheckerInfo()
        {
            monsterAttackCheckers[0].SetMonsterData(monsterData);
            monsterAttackCheckers[0].SetMonsterType(monsterType);
        }
    }

    protected virtual IEnumerator Die()
    {
        OnDeadSettings();

        SoundManager.Instance.PlayClip($"{monsterData.monsterName}Die", transform);
        animator.SetTrigger("IsDead");

        InstantiateEffect();

        yield return YieldCache.GetCachedTimeInterval(destroyDelayTime);
        ResourceManager.Instance.Destroy(gameObject);
        StopAllCoroutines();

        void OnDeadSettings()
        {
            isDead = true;
            monsterCollider.enabled = false;
            rigidBody.isKinematic = true;

            EntityManager.Instance.currencyData.AddGold(monsterData.goldValue);
            EntityManager.Instance.player.GetExpValues(monsterData.expValue);
        }
        void InstantiateEffect()
        {
            GameObject deadEffect = ResourceManager.Instance.Instantiate("DeadEffect", gameObject.transform);
            deadEffect.transform.localPosition = Vector3.zero;
            deadEffect.transform.localRotation = Quaternion.identity;
        }
    }

    protected IEnumerator CheckIsDead()
    {
        while(true)
        {
            if (this.monsterHealth <= 0)
            {
                ChangeState(MonsterState.Die);
                break;
            }

            if(dieFlag)
            {
                this.monsterHealth = 0;
                healthBar.SetHealth(this.monsterHealth);
                ChangeState(MonsterState.Die);
                break;
            }

            yield return null;
        }
    }

    public virtual void GetDamaged(float damage)
    {
        this.monsterHealth -= (damage - monsterArmor);
        healthBar.SetHealth(monsterHealth);
        
        if(monsterType != Define.ObjectType.Boss)
        {
            animator.SetTrigger("GetDamaged");
            SoundManager.Instance.PlayClip($"{monsterData.monsterName}Damaged",  transform);
        }
    }

    public GameObject GetOnHitEffectPoint() => this.onHitEffectPoint;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NpcController : MonoBehaviour
{
    [SerializeField] private NpcData npcData;
    [SerializeField] private Animator npcAnim;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool useTalk;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            CombatUI.setInteractionButton?.Invoke();
            if (useTalk == true)
            {
                LookPlayer(other);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            NpcManager.Instance.SetNpcData(npcData);
            if (useTalk == true)
            {
                npcAnim.SetTrigger("talk");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CombatUI.setAttackButton?.Invoke();
    }

    private void LookPlayer(Collider other)
    {
        Vector3 lookDir = (other.transform.position - transform.position).normalized;

        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(lookDir);

        transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * rotateSpeed);
    }
}

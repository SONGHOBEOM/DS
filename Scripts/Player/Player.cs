using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCORE;
using System;

[Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private Transform armorPoint;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;

    public int level { get; private set; } = 1;
    public int questLevel { get; private set; } = 1;
    public float defense { get; private set; } = 0;
    public float minDamage { get; private set; }
    public float maxDamage { get; private set; }
    public float maxHp { get; private set; } = 1000;
    public float curHp { get; private set; } = 1000;
    public float curExp { get; private set; }

    private Weapon weapon;
    private Armor armor;

    private float attackDelay = 0;
    private bool enableAttack = true;

    public void SavePlayer() => SaveSystem.SavePlayer(this);

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        SetPlayerInfo();
        SetTransform();
        SetUISettings();

        void SetPlayerInfo()
        {
            level = data.GetPlayerLevel();
            questLevel = data.GetPlayerQuestLevel();
            maxHp = data.GetPlayerMaxHp();
            curHp = data.GetPlayerCurHp();
            curExp = data.GetPlayerCurExp();
        }
        void SetTransform()
        {
            //Set Player Position
            Vector3 position = new Vector3();
            for (int i = 0; i < data.GetPlayerPosition().Length; i++)
                position[i] = data.GetPlayerPosition()[i];

            transform.position = position;

            //Set Player Rotation
            Quaternion rotation = new Quaternion();
            for (int i = 0; i < data.GetPlayerRotation().Length; i++)
                rotation[i] = data.GetPlayerRotation()[i];

            transform.rotation = rotation;

            // Set Camera Rotation
            Quaternion cameraRotation = new Quaternion();

            for (int i = 0; i < data.GetCameraRotation().Length; i++)
                cameraRotation[i] = data.GetPlayerRotation()[i];

            var controller = gameObject.GetComponent<PlayerStateController>().GetMainCamera().transform.parent.GetComponent<CameraMoveController>();
            controller.SetInitCameraRotation(rotation);

        }
        void SetUISettings()
        {
            InventoryUI.refreshList?.Invoke();
            QuestManager.Instance.UpdateQuestlistItem(questLevel);
            PortraitUI.updateValues?.Invoke();
        }
    }

    public Transform GetCameraTarget() => cameraTarget;
    public Quaternion GetCameraRotation()
    {
        var controller = gameObject.GetComponent<PlayerStateController>();
        var cameraRotation = controller.GetMainCamera().transform.parent.rotation;
        return cameraRotation;
    }
    public void LevelUp() => level++;
    public void QuestLevelUp() => questLevel++;
    public void GetExpValues(float value)
    {
        curExp += value;
        PortraitUI.updateExpValue?.Invoke();
    }
    public void Equip(Equipment equipmentItem)
    {
        if (equipmentItem.itemData.itemType == Define.ItemType.Armor)
        {
            armor = equipmentItem as Armor;
            EquipArmor(armor);
        }
        else if (equipmentItem.itemData.itemType == Define.ItemType.Weapon)
        {
            weapon = equipmentItem as Weapon;
            EquipWeapon(equipmentItem);
        }
    }

    public void UnEquip(Equipment equipmentItem)
    {
        if (equipmentItem.itemData.itemType == Define.ItemType.Armor)
        {
            UnEquipArmor();
        }
        else if (equipmentItem.itemData.itemType == Define.ItemType.Weapon)
        {
            UnEquipWeapon();
        }
    }

    public void EquipWeapon(ItemBase itemData)
    {
        if (itemData == null) return;

        var weaponData = itemData.itemData as WeaponData;

        UnEquipWeapon();
        EquipWeapon(weaponData);
    }
    private void EquipWeapon(WeaponData weaponData)
    {
        weapon = ResourceManager.Instance.Instantiate<Weapon>(weaponData.name, weaponPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        PlayerAnimationManager.Instance.RunPlayerEquipWeapon(weaponData);
        AddWeaponStat(weaponData);
        PortraitUI.updateValues?.Invoke();
        PlayerStatUI.updatePlayerStat?.Invoke();
    }

    private void AddWeaponStat(WeaponData weaponData)
    {
        this.maxDamage += weaponData.maxDamage;
        this.minDamage += weaponData.minDamage;
        this.maxHp += weaponData.maxHp;
        this.defense += weaponData.defense;
    }

    private void SubWeaponStat(WeaponData weaponData)
    {
        this.maxDamage -= weaponData.maxDamage;
        this.minDamage -= weaponData.minDamage;
        this.maxHp -= weaponData.maxHp;
        this.defense -= weaponData.defense;
    }

    private void UnEquipWeapon()
    {
        if (weaponPoint.childCount == 0) return;

        var weaponData = weapon.itemData as WeaponData;
        SubWeaponStat(weaponData);

        weapon = default;
        PlayerStatUI.updatePlayerStat?.Invoke();
        PlayerAnimationManager.Instance.RunPlayerUnEquipWeapon();

        GameObject equippedWeapon = weaponPoint.GetChild(0).gameObject;
        ResourceManager.Instance.Destroy(equippedWeapon);

        SkillBase.setOff?.Invoke();
        PortraitUI.updateValues?.Invoke();
    }

    public void EquipArmor(ItemBase itemData)
    {
        if (itemData == null) return;

        var armorData = itemData.itemData as ArmorData;
        EquipArmor(armorData);
    }
    private void EquipArmor(ArmorData armorData)
    {
        GameObject armor = ResourceManager.Instance.Instantiate(armorData.itemName, armorPoint);
        armor.transform.localPosition = Vector3.zero;
        armor.transform.localRotation = Quaternion.identity;
        AddArmornStat(armorData);
        PortraitUI.updateValues?.Invoke();
        PlayerStatUI.updatePlayerStat?.Invoke();
    }
    private void AddArmornStat(ArmorData armorData)
    {
        this.maxHp += armorData.maxHp;
        this.defense += armorData.defense;
    }

    private void SubArmorStat(ArmorData armorData)
    {
        this.maxHp -= armorData.maxHp;
        this.defense -= armorData.defense;
    }
    private void UnEquipArmor()
    {
        var armorData = armor.itemData as ArmorData;
        SubArmorStat(armorData);

        armor = default;
        PlayerStatUI.updatePlayerStat?.Invoke();
        GameObject equippedArmor = armorPoint.GetChild(0).gameObject;
        ResourceManager.Instance.Destroy(equippedArmor);
        PortraitUI.updateValues?.Invoke();
    }

    public Weapon GetPlayerWeapon()
    {
        if(weapon == null) return default;
        return weapon;
    }

    public void BasicAttackWithDelay()
    {
        if (enableAttack == false)
            return;

        StartCoroutine(BasicAttack());
    }

    public void GetDamaged(float damage)
    {
        curHp -= damage;
        PortraitUI.updateValues?.Invoke();
        StartCoroutine(DamagedEffect());
    }

    private IEnumerator DamagedEffect()
    {
        PlayerStateHelper.isDamaged = true;
        ChangeColor(UnityEngine.Color.red);

        float effectDuration = 0.5f;
        yield return YieldCache.GetCachedTimeInterval(effectDuration);

        PlayerStateHelper.isDamaged = false;
        ChangeColor(UnityEngine.Color.white);


        void ChangeColor(UnityEngine.Color color)
        {
            foreach (var renderer in skinnedMeshRenderers)
            {
                var materials = renderer.materials;
                foreach (var material in materials)
                    material.color = color;
            }
        }
    }

    private IEnumerator BasicAttack()
    {
        if(weapon == null)
            yield break;

        PlayerStateHelper.isAttacking = true;
        SoundManager.Instance.PlayClip("PlayerAttack");
        enableAttack = false;
        weapon.ActivateChecker();
        playerAnimator.SetTrigger("BasicAttack");

        attackDelay = weapon.GetWeaponData().attackDelay;
        yield return YieldCache.GetCachedTimeInterval(attackDelay);

        weapon.UnActivateChecker();
        enableAttack = true;
        PlayerStateHelper.isAttacking = false;
    }

}

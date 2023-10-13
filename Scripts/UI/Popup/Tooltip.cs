using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject weaponValues;
    [SerializeField] private GameObject armorValues;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text rarityText;
    [SerializeField] private Text classText;
    [SerializeField] private Text itemTypeText;
    [SerializeField] private Text weaponEquipLevelText;
    [SerializeField] private Text armorEquipLevelText;
    [SerializeField] private Text minimumDamageText;
    [SerializeField] private Text maximumDamageText;
    [SerializeField] private Text weaponMaxHpText;
    [SerializeField] private Text armorMaxHpText;
    [SerializeField] private Text weaponDefenseText;
    [SerializeField] private Text armorDefenseText;
    [SerializeField] private Text descriptionText;

    float halfwidth;
    RectTransform rt;

    public static Action onDisabled = null;
    private void Start()
    {
        halfwidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        rt = GetComponent<RectTransform>();
        onDisabled = Disabled;
        
    }
    private void Update()
    {
        transform.position = Input.mousePosition;

        if (rt.anchoredPosition.x + rt.sizeDelta.x > halfwidth)
            rt.pivot = new Vector2(1, 1);
        else
            rt.pivot = new Vector2(0, 1);
    }
    public void SetupTooltipInfo(ItemBase itemBase)
    {
        switch(itemBase.itemData.itemType)
        {
            case Define.ItemType.Weapon:
                SetupWeaponInfo(itemBase);
                armorValues.SetActive(false);
                weaponValues.SetActive(true);
                break;
            case Define.ItemType.Armor:
                SetupArmorInfo(itemBase);
                weaponValues.SetActive(false);
                armorValues.SetActive(true);
                break;
        }

    }
    private void SetupWeaponInfo(ItemBase itemBase)
    {
        itemImage.sprite = itemBase.itemData.itemImage;
        nameText.text = itemBase.itemData.itemName;
        itemTypeText.text = itemBase.itemData.itemType.ToString();
        var weapondata = itemBase.itemData as WeaponData;
        rarityText.text = weapondata.rarity.ToString();
        SetRarityColor(weapondata.rarity);
        classText.text = weapondata.playerClass.ToString();
        weaponEquipLevelText.text = string.Format(STR.Get("EquipLevel"), weapondata.equipLevel);
        maximumDamageText.text = string.Format(STR.Get("MaximumDamage"), weapondata.maxDamage);
        minimumDamageText.text = string.Format(STR.Get("MinimumDamage"), weapondata.minDamage);
        weaponMaxHpText.text = string.Format(STR.Get("PlayerMaxHp"), weapondata.maxHp);
        weaponDefenseText.text = string.Format(STR.Get("PlayerDefense"), weapondata.defense);
        descriptionText.text = weapondata.description.ToString();
    }
    private void SetupArmorInfo(ItemBase itemBase)
    {
        itemImage.sprite = itemBase.itemData.itemImage;
        nameText.text = itemBase.itemData.itemName;
        itemTypeText.text = itemBase.itemData.itemType.ToString();
        var armorData = itemBase.itemData as ArmorData;
        rarityText.text = armorData.rarity.ToString();
        SetRarityColor(armorData.rarity);
        armorEquipLevelText.text = string.Format(STR.Get("EquipLevel"), armorData.equipLevel);
        armorMaxHpText.text = string.Format(STR.Get("PlayerMaxHp"), armorData.maxHp);
        armorDefenseText.text = string.Format(STR.Get("PlayerDefense"), armorData.defense);
    }
    public void SetRarityColor(Define.Rarity rarity)
    {
        if (rarity == Define.Rarity.Normal) { rarityText.color = Color.grey; }
        else if (rarity == Define.Rarity.Rare) { rarityText.color = Color.blue; }
        else if (rarity == Define.Rarity.Epic) { rarityText.color = Color.magenta; }
        else if (rarity == Define.Rarity.Heroic) { rarityText.color = Color.yellow; }
        else if (rarity == Define.Rarity.Legendary) { rarityText.color = Color.red; }
    }

    private void OnEnable()
    {
        gameObject.transform.SetAsLastSibling();
    }
    private void Disabled() => gameObject.SetActive(false);
}

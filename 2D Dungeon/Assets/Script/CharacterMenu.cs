using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText;
    public Text coinText;
    public Text hpText;
    public Text xpText;
    public Text upgradeCostText;

    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.instance.playerSprite.Count)
            {
                currentCharacterSelection = 0;
            }
        }
        else
        {
            currentCharacterSelection--;
            if(currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprite.Count - 1;
            }
        }

        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprite[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrice.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrice[GameManager.instance.weapon.weaponLevel].ToString();


        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hpText.text = GameManager.instance.player.hitPoint.ToString() + " / " + GameManager.instance.player.maxHitPoint.ToString();
        coinText.text = GameManager.instance.coins.ToString();

        int currlv = GameManager.instance.GetCurrentLevel();
        if (currlv == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            int prevxp = GameManager.instance.GetXpToLevel(currlv - 1);
            int currxp = GameManager.instance.GetXpToLevel(currlv);

            int diff = currxp - prevxp;
            int exptolevel = GameManager.instance.experience - prevxp;
            float ratio = (float)exptolevel / (float)diff;

            xpText.text = exptolevel.ToString() + " / " + diff.ToString();
            xpBar.localScale = new Vector3((float)ratio, 1, 1);
        }
    }
}

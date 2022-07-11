using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Upgrader : MonoBehaviour
{
    public float stayLength;
    public GameObject skillBtn;
    public List<LevelUpgradePair> upgrades;

    private Text text;
    private Animation ani;
    private int currentLevel = 1;
    private PlayerFire playerFire;
    private GameUnit playerUnit;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GlobalData.player);
        playerFire = GlobalData.player.GetComponent<PlayerFire>();
        playerUnit = GlobalData.player.GetComponent<GameUnit>();
        text = GetComponent<Text>();
        ani = GetComponent<Animation>();

        MakeText("消灭所有色块!");

        StartCoroutine(Upgrade());
    }

    private IEnumerator Upgrade()
    {
        while (!GlobalData.isGameOver)
        {
            yield return new WaitUntil(() => Mathf.RoundToInt(GlobalData.score) / 100 >= currentLevel);
            currentLevel++;

            DoUpgrade(GetUpgradeByLvl(currentLevel));
        }
    }

    private LevelUpgradePair GetUpgradeByLvl(int level)
    {
        foreach (var item in upgrades)
        {
            if (currentLevel == item.lvl) return item;
        }

        return upgrades[upgrades.Count - 1];
    }

    private void DoUpgrade(LevelUpgradePair item)
    {
        switch (item.type)
        {
            case LevelUpgradePair.UpgradeType.Gun_Upgrade:
                MakeText("机枪已升级!");
                playerFire.gunLevel++;
                playerFire.gunLevel = Mathf.Clamp(playerFire.gunLevel, 1, 3);
                break;

            case LevelUpgradePair.UpgradeType.Shield:
                MakeText("护盾容量增加!");
                playerUnit.maxShield += item.value;
                break;

            case LevelUpgradePair.UpgradeType.HP_Up:
                MakeText("血量增加! ");
                playerUnit.maxHp += item.value;
                playerUnit.hp = playerUnit.maxHp;
                break;

            case LevelUpgradePair.UpgradeType.Restore:
                MakeText("血量回复! ");
                playerUnit.hp += playerUnit.maxHp * item.value;
                playerUnit.hp = Mathf.Clamp(playerUnit.hp, 0, playerUnit.maxHp);
                break;

            case LevelUpgradePair.UpgradeType.Skill:
                MakeText("获得技能! ");
                skillBtn.SetActive(true);
                break;
        }
    }

    private void MakeText(string text)
    {
        ani.Stop();
        this.text.text = text;
        StartCoroutine(TextAnimator());
    }

    private IEnumerator TextAnimator()
    {
        ani.Play("UpgradeText_In");
        yield return new WaitForSeconds(ani["UpgradeText_In"].length + stayLength);
        ani.Play("UpgradeText_Out");
    }
}

[System.Serializable]
public struct LevelUpgradePair
{
    public int lvl;
    public UpgradeType type;
    public float value;

    public enum UpgradeType
    {
        Gun_Upgrade, Shield, HP_Up, Restore, Skill
    }
}

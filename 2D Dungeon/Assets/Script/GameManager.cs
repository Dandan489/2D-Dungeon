using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(Hud);
            Destroy(Menu);
            return;
        }

        PlayerPrefs.DeleteAll();

        instance = this;
        // sceneLoaded gonna run after loading is over
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoad;
        DontDestroyOnLoad(gameObject);
    }

    //resources
    public List<Sprite> playerSprite;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrice;
    public List<int> xpTable;
    
    //ref
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject Menu;
    public GameObject Hud;

    //logic
    public int coins;
    public int experience;
    public int preferedSkin;
    public int weaponLevel;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        if (weaponPrice.Count <= weapon.weaponLevel)
            return false;

        if(coins >= weaponPrice[weapon.weaponLevel])
        {
            coins -= weaponPrice[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }

        return r;
    }

    public void GrantXp(int xp)
    {
        int level = GetCurrentLevel();
        experience += xp;
        if(level != GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        Debug.Log("up");
        player.OnLevelUp();
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void OnHitpointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    public void OnSceneLoad(Scene s, LoadSceneMode mode)
    {
        GameManager.instance.ShowText("LEVEL " + (SceneManager.GetActiveScene().buildIndex-100), 50, Color.black, new Vector3(0, 0.16f, 0), Vector3.zero, 1.0f);
    }

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("saved");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        coins = int.Parse(data[1]);

        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("Spawn").transform.position;

        Debug.Log("loaded");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health, maxHealth;
    //public int stamina, maxStamina;
    public string[] items;
    public Slider healthBar, staminaBar;

    private void Start()
    {
        health = GlobalStats.Instance.HP;
        //stamina = GlobalStats.Instance.stamina;
        items = GlobalStats.Instance.items;

        if (healthBar)
        {
            healthBar.value = health;
        }

        //if (staminaBar)
        //{
        //    staminaBar.maxValue = maxStamina;
        //    staminaBar.value = stamina;
        //}
    }

    public void SavePlayer()
    {
        GlobalStats.Instance.HP = health;
        //GlobalStats.Instance.stamina = stamina;
        GlobalStats.Instance.items = items;
        GlobalStats.scene1Visited = true;
    }

    public int GetHealth()
    {
        return health;
    }

    //public int GetStamina()
    //{
    //    return stamina;
    //}

    public Transform GetPosition()
    {
        return gameObject.transform;
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Lowers the players health by the provided factor
    /// </summary>
    /// <param name="damage">The factor by which health is to be reduced.</param>
    public void DealDamage(int damage)
    {
        health -= damage;

        if (healthBar)
        {
            healthBar.value = health;
        }

        if (health <= 0)
        {
            // Display game over screen
            Destroy(gameObject); // Temporary solution
        }
    }

    public void AddItem(string item)
    {
        if (items[items.Length - 1] != null)
        {
            string[] temp = items;
            int i = 0;
            items = new string[temp.Length + 10];
            for (i = 0; i < temp.Length; i++)
            {
                items[i] = temp[i];
            }
            i = 0;
            while (items[i] != null)
            {
                i++;
            }
            print(i);
            items[i] = "blyat";
        }
        else
        {
            int i = 0;
            while (items[i] != null)
            {
                i++;
            }
            items[i] = item;
        }
    }

}

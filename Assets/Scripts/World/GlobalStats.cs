using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalStats : MonoBehaviour
{
    public int HP;
    public int stamina;

    public string[] items;

    public static GlobalStats Instance;

    public static bool scene1Visited = false;

    private void Awake()
    {
        items = new string[10];
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");

        PlayerData data = new PlayerData();
        data.health = GameObject.Find("Amie").GetComponent<PlayerStats>().health;
        //data.stamina = GameObject.Find("Amie").GetComponent<PlayerStats>().stamina;
        data.scene = SceneManager.GetActiveScene().name;
        data.posX = GameObject.Find("Amie").GetComponent<Transform>().position.x;
        data.posY = GameObject.Find("Amie").GetComponent<Transform>().position.y;
        data.posZ = GameObject.Find("Amie").GetComponent<Transform>().position.z;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {
            print(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            HP = data.health;
            stamina = data.stamina;
            if (SceneManager.GetActiveScene().name != data.scene)
            {
                SceneManager.LoadScene(data.scene);
            }

            GameObject.Find("Amie").transform.position = new Vector3(data.posX, data.posY, data.posZ);
        }
    }

}

[Serializable]
class PlayerData
{
    public int health;
    public int stamina;
    public float posX, posY, posZ;
    public string scene;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance = null;

    public float brightness;
    public float saturation;
    public float musicVolume;
    public float soundVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData");

        SaveData data = new SaveData();

        data.completedLevels = GameManager.instance.completedLevels;
        data.brightness = brightness;
        data.saturation = saturation;
        data.soundVolume = soundVolume;
        data.soundVolume = musicVolume;

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData", FileMode.Open);

            SaveData data = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();

            SceneManager.LoadScene("Puzle 1");
            
            GameManager.instance.completedLevels = data.completedLevels;

            GameManager.instance.brightness = data.brightness;
            GameManager.instance.ChangeBrightness();
            Debug.Log(data.brightness);

            GameManager.instance.saturation = data.saturation;
            GameManager.instance.ChangeSaturation();
            Debug.Log(data.saturation);

            for (int i = 0; i < GameManager.MAX_LEVELS; ++i)
            {
                Debug.Log(GameManager.instance.completedLevels[i]);
            }
        }
        else
        {
            Debug.Log("No existe ninguna partida guardada");
        }
    }
}

[Serializable]
class SaveData
{
    public bool[] completedLevels;
    public float brightness;
    public float saturation;
    public float musicVolume;
    public float soundVolume;
}

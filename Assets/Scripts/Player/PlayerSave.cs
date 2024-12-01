using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public class PlayerSave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float saveInterval;
    float currentCooldownSecond = 0.0f;
    FileStream my_stream;
    string file_path;
    void Start()
    {

        LoadGame();

    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        file_path = Application.persistentDataPath + "/player.save";
    }
    // Update is called once per frame
    void Update()
    {
        SaveGame();
    }
    private void SaveGame()
    {

        if (currentCooldownSecond > 0)
        {
            currentCooldownSecond -= Time.deltaTime;
            return;
        }
        SavedData sav = new SavedData(transform.position, 1);
        BinaryFormatter file_converter = new BinaryFormatter();
        using (my_stream = new FileStream(file_path, FileMode.Create))
        {

            file_converter.Serialize(my_stream, sav);
            my_stream.Close();
        }
        Debug.Log(file_path);
        currentCooldownSecond = saveInterval;
    }
    private void LoadGame()
    {
        SavedData sav;
        BinaryFormatter file_converter = new BinaryFormatter();
        if (File.Exists(file_path))
        {
            try
            {
                using (my_stream = new FileStream(file_path, FileMode.Open))
                {
                    sav = (SavedData)file_converter.Deserialize(my_stream);
                    transform.position = new Vector3(sav.playerX, sav.playerY, sav.playerZ);
                }
            }
            catch (SerializationException e)
            {
                File.Delete(file_path);
            }


        }
        else
        {
            Debug.Log("non existent file path meow");
        }
    }
}
[Serializable]
public class SavedData
{

    public float playerX;
    public float playerY;
    public float playerZ;

    public int level;
    public SavedData(Vector3 playerCoordinate, int level)
    {
        this.playerX = playerCoordinate.x;
        this.playerY = playerCoordinate.y;
        this.playerZ = playerCoordinate.z;
        this.level = level;
    }
}
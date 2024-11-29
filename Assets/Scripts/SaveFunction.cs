using System;
using UnityEngine;

public class SaveFunction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int saveInterval;
    public int currentSaveTime;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
[Serializable]
public class SavedData
{
    Vector2 playerCoordinate;
    int level;

}
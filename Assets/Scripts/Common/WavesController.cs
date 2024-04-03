using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [SerializeField] private string dataFile;
    private List<Dictionary<string, object>> wavesData = new();

    // Start is called before the first frame update
    void Start()
    {
        wavesData = CSVReader.Read(dataFile);

        for (int i = 0; i < wavesData.Count; i++)
        {
            Debug.Log("Wave: " + wavesData[i]["wave"] + " Enemy1 count: " + wavesData[i]["enemy1"] + " Enemy2 count: " + wavesData[i]["enemy2"]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [SerializeField] private string filenameCsv;
    [SerializeField] private string filenameJson;
    private List<Dictionary<string, object>> wavesData = new();
    private JSONWaves wavesDataJson = new();

    // Start is called before the first frame update
    void Start()
    {
        // CSV version
        wavesData = CSVParser.Read(filenameCsv);

        for (int i = 0; i < wavesData.Count; i++)
        {
            Debug.Log("Wave: " + wavesData[i]["wave"] + " Enemy1 count: " + wavesData[i]["enemy1"] + " Enemy2 count: " + wavesData[i]["enemy2"]);
        }

        //JSON version
        wavesDataJson = JSONWaves.CreateFromJSON(TextFileReader.ReadFileAsText(filenameJson));

        Debug.Log("Wave: " + wavesDataJson.waves[0].id + " Enemy1 id: " + wavesDataJson.waves[0].enemies[0].id + " Enemy1 spawnTime: " + wavesDataJson.waves[0].enemies[0].spawnTime);
    }
}

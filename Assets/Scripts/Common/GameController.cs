using MEC;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private GameObject panelScore;
    [SerializeField]
    private GameObject lifePanel;
    [SerializeField]
    private GameObject cannonPanel;
    [SerializeField]
    private TMP_Text textScore;
    public int currentScore=0;
    [SerializeField]
    PoolController poolController;
    [SerializeField]
    public GameObject ammoImage1;
    public GameObject ammoImage2;
    public GameObject ammoImage3;
    [SerializeField]
    public GameObject lifeImage1;
    public GameObject lifeImage2;
    public GameObject lifeImage3;
    private int lifeCount=3;
    public GameObject GameOverPanel;
   

    public void UpdateScore()
    {
        currentScore++;
        textScore.text=currentScore.ToString();
    }

    public void UpdateLife()
    {
        lifeCount--;
        switch (lifeCount)
        {
            case 0:
                lifeImage1.SetActive(false);
                playerInput.enabled=false;   
                StopAllCoroutinesInScene();
                GameOverPanel.SetActive(true);
                break;

            case 1:
                lifeImage2.SetActive(false);
                break;
            case 2:
                lifeImage3.SetActive(false);
                break;
            case 3:
                lifeImage1.SetActive(true);
                lifeImage2.SetActive(true);
                lifeImage3.SetActive(true);
                break;


        }
    }

    public void UpdateAmmo(int ammoCount)
    {
        switch (ammoCount)
        {
            case 0:
              ammoImage1.SetActive(false);
                break; 
            
            case 1:
                ammoImage2.SetActive(false);
                break;
            case 2:
                ammoImage3.SetActive(false);
                break;
            case 3:
                ammoImage1.SetActive(true);
                ammoImage2.SetActive(true);
                ammoImage3.SetActive(true);
                break;


        }
    }

  public void Restart()
    {
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void StopAllCoroutinesInScene()
    {
        // Trova tutti i GameObject nella scena
        GameObject[] allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        // Itera su tutti i GameObject e ferma le coroutine attive su ognuno di essi
        foreach (GameObject obj in allObjects)
        {
            Timing.KillCoroutines(obj);
           
        }

    }
  
}

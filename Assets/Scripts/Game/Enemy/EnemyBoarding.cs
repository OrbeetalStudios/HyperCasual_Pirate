using MEC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBoarding : MonoBehaviour
{
    [SerializeField]
    GameObject enemyObj;
    [SerializeField]
    private int Countdown=5;
    private int resetCount;
    private bool playerInside=false;
    private bool startCount=false;

    private void OnEnable()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    private void Start()
    {
        resetCount = Countdown;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.green;
            playerInside = true;
            if (startCount == false)
            {
                Timing.RunCoroutine(countDownCoroutine());
            }
        }  
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.red;
            playerInside = false;
            ResetCount();
        }
    }

    public void ResetCount()
    {
      Countdown=resetCount;
    }

    protected IEnumerator<float> countDownCoroutine()
    {
        startCount = true;
        while (playerInside==true)
        {
           
            Countdown--;
            if (Countdown ==0)
            {
                enemyObj.SetActive(false);
                playerInside =false;   
            }
            Debug.Log("Tempo " + Countdown);
            yield return Timing.WaitForSeconds(1f);
        }
        startCount=false;
    }

    private void OnDisable()
    {
        playerInside = false;
        startCount = false;
        StopCoroutine("countDownCoroutine");
        ResetCount();
    }
}

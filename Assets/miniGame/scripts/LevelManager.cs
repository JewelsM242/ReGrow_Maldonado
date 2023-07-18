using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Battery battery;
    [Header("linesInTheScene")]
    public GameObject[] lines;
    public bool done = false;


    public void initialize(){
        lines = GameObject.FindGameObjectsWithTag("Line");
    }

    private void Start()
    {
        lines = GameObject.FindGameObjectsWithTag("Line");
    }
    public void StartPowerCommand()
    {
        //reset all the lines in the scene
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].GetComponent<Line>().ResetStatic();
        }

        //Ontrigger has delay
        Invoke("StartPower", 0.5f);

        //time for the recursion
        Invoke("isGameOver", 0.5f);
    }

    private void StartPower()
    {
        battery.StartPower();
    }

    private void isGameOver()
    {
        GameObject gameObject = battery.inLine;
        if (gameObject != null)
        {
            if (gameObject.GetComponent<Line>().isPower)
                GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        GameManager.endMicrogame();
    }
}


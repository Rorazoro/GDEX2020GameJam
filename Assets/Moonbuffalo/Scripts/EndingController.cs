using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : SingletonBehaviour<MonoBehaviour>
{
    public Transform AllEndingStuff;
    public List<GameObject> Pandas;
    public Transform PandaContainer;
    private bool GameEnded = false;
    private bool InGameEnd = false;

    public void Start()
    {
        foreach(var panda in Pandas)
        {
            panda.SetActive(false);
        }
    }

    public void Update()
    {
        int numPandas = PandaContainer.childCount;
        for (int i = 0; i<numPandas && i < Pandas.Count; i++)
        {
            Pandas[i].SetActive(true);
        }

        if (numPandas>=30 && !GameEnded)
        {
            EndGame();
        }

        
        if (InputManager.Instance.CastInput && InGameEnd)
        {
            CameraManager.Instance.ToggleCamera(0);
            InGameEnd = false;
        }
    }

    public void EndGame()
    {
        GameEnded = true;
        InGameEnd = true;
        AllEndingStuff.gameObject.SetActive(true);
        CameraManager.Instance.ToggleCamera(3);
    }

}

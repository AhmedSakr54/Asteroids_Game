﻿using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    float elapsedSeconds = 0;
    bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;

            scoreText.text = ((int)elapsedSeconds).ToString();
        }
    }

    public void stopGameTimer()
    {
        running = false;
    }
}

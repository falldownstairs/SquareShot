using UnityEngine;
using TMPro;
using System;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool rollTime = true;
    public static Timer Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
    }
    void Start()
    {
        startTime = Time.time;

    }

    void Update()
    {
        if (rollTime){
            string output = "";
            float t = Time.time - startTime;
            float minutes = ((int)t / 60);
            double seconds = t % 60;
            string min = minutes.ToString();
            string sec = seconds.ToString("f0");
            if (minutes > 10){
                output += min + ":";
            }
            else{
                output += "0" + min + ":";
            }
            if (seconds > 10){
                output += sec;
            }
            else{
                output += "0" + sec;
            }
            timerText.text = output;
        }
    }
    public string EndTimer(){
        rollTime = false;
        return timerText.text;

    }
}
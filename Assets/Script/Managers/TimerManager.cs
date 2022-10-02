using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    [SerializeField] float timer = 10.0f;
    [SerializeField, Tooltip("Use to handle how fast the time is ticking")] float timeFactor = 1.0f;
    public float Timer {
        get {return timer;}
    }
    public bool isTimerStarted = false;
    int lastTimerTick;

    public UnityEvent timerElapsed;
    public UnityEvent<int> timerTick;

    // Start is called before the first frame update
    void Start()
    {
        lastTimerTick = Mathf.RoundToInt(timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerStarted) {
            timer -= Time.deltaTime * timeFactor;

            int newTick = Mathf.RoundToInt(timer);
            if (newTick != lastTimerTick) {
                timerTick?.Invoke(lastTimerTick);
                lastTimerTick = newTick;
            }

            if (timer <= 0.0f) {
                isTimerStarted = false;
                timerElapsed?.Invoke();
            }
        }
    }

    public void startTimer() {
        isTimerStarted = true;
    }

    public void resetTimer() {
        isTimerStarted = false;
        timer = 10.0f;
        lastTimerTick = Mathf.RoundToInt(timer);
        timerTick?.Invoke(lastTimerTick);
    }
}

using UnityEngine;

public class TimeManager : MonoBehaviour {
    
    public const float MILLISECOND = 0.001f;
    public const float SECOND = 1f;
    public const float MINUTE = 60f;
    public const float HOUR = 3600f;
    public const float DAY = 86400f;

    public void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Debug.Log("Pausing Time");
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Debug.Log("Resuming Time");
        Time.timeScale = 1f;
    }

    public void Toggle()
    {
        Debug.Log("Toggling Time");
        if(Time.timeScale == 0f)
        {
            Resume();
        } else
        {
            Pause();
        }
    }

    public void SetTimeScale(float scale)
    {
        Debug.Log("Setting Time Scale to " + scale);
        Time.timeScale = scale;
    }

    public float CurrentTime()
    {
        return Time.time;
    }

    public void FixedUpdate()
    {
        // ? increase time scale by 0.1 every 5 seconds
        /*
        if(Time.time % 5 == 0)
        {
            Time.timeScale += 0.1f;
        }
        */
    }




}
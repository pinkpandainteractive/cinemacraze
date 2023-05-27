using UnityEngine;

public class TimeManager : MonoBehaviour {
    
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

    public void SetTimeScale(float scale)
    {
        Debug.Log("Setting Time Scale to " + scale);
        Time.timeScale = scale;
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
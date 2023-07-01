using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    public Slider slider;
    private float targetProgress = 0;
    private float currentProgress = 0;
    private void Start()
    {
        slider.GetComponent<Slider>().interactable = false;
        slider.gameObject.SetActive(false);
    }
    public void SliderProgress(float productionTime)
    {
        if (currentProgress < targetProgress)
        {
            float increment = Time.deltaTime / productionTime;
            currentProgress = Mathf.Min(currentProgress + increment, targetProgress);
            slider.value = currentProgress;
        }
    }
    public void IncrementProgress(float progress)
    {
        targetProgress = Mathf.Clamp01(targetProgress + progress);
    }

    public void ResetProgressbar()
    {
        currentProgress = 0;
        targetProgress = 0;
        slider.gameObject.SetActive(false);
    }
}

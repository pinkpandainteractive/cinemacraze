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
        HandleVisbilityHidden();
    }
    public IEnumerator SliderProgress(float productionTime)
    {
        while (currentProgress < targetProgress)
        {
            float increment = Time.deltaTime / productionTime;
            currentProgress = Mathf.Min(currentProgress + increment, targetProgress);
            slider.value = currentProgress;
            yield return null;
        }
    }
    public void IncrementProgress(float progress)
    {
        targetProgress = Mathf.Clamp01(targetProgress + progress);
    }
    public void ProgressbarColor(Color color)
    {
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }
    public void HandleVisbilityHidden()
    {     
        slider.gameObject.SetActive(false);   
    }
    public void HandleVisbilityVisible()
    {
        slider.gameObject.SetActive(true);
    }
    public void ResetProgressbar()
    {
        currentProgress = 0;
        targetProgress = 0;
        HandleVisbilityHidden();
    }
}

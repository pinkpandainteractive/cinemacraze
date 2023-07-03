using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DynamicText : MonoBehaviour
{
    public TMP_Text textComponent;
    public Image imageComponent;

    public GameObject imagePrefab;
    public Sprite popcornSprite;
    public Sprite sodaSprite;
    public Sprite nachoSprite;
    private Dictionary<string, Sprite> imageDictionary = new Dictionary<string, Sprite>();


    /*void Start()
    {
        string text = textComponent.text;

        // Prüfe auf "Popcorn" und aktualisiere das Bild
        if (Regex.IsMatch(text, "Popcorn", RegexOptions.IgnoreCase))
        {
            textComponent.text = Regex.Replace(text, "Popcorn", "<sprite=0>", RegexOptions.IgnoreCase);
            imageComponent.sprite = popcornSprite;
            imageComponent.enabled = true;
        }

        // Prüfe auf "Soda" und aktualisiere das Bild
        if (Regex.IsMatch(text, "Soda", RegexOptions.IgnoreCase))
        {
            textComponent.text = Regex.Replace(text, "Soda", "<sprite=1>", RegexOptions.IgnoreCase);
            imageComponent.sprite = sodaSprite;
            imageComponent.enabled = true;
        }
    }*/
    /*void Start()
    {
        // Erstelle eine Dictionary-Map zwischen Textausdrücken und Sprites
        imageDictionary.Add("Popcorn", popcornSprite);
        imageDictionary.Add("Soda", sodaSprite);
        imageDictionary.Add("Nacho", nachoSprite);

        string text = textComponent.text;

        // Trenne den Text in separate Segmente
        string[] segments = Regex.Split(text, @"(?<=\b\S+\b)");

        // Erstelle eine neue Liste für die modifizierten Segmente
        List<string> modifiedSegments = new List<string>();

        // Durchsuche die Segmente nach den passenden Ausdrücken
        foreach (string segment in segments)
        {
            bool expressionFound = false;
            string trimmedSegment = segment.Trim(); // Leerzeichen am Anfang und Ende entfernen

            // Suche nach den passenden Ausdrücken in jedem Segment
            foreach (KeyValuePair<string, Sprite> entry in imageDictionary)
            {
                if (Regex.IsMatch(trimmedSegment, @"\b" + entry.Key + @"\b", RegexOptions.IgnoreCase))
                {
                    string modifiedSegment = $"{GetWordCount(trimmedSegment)}x \"{entry.Key} image\"";
                    modifiedSegments.Add(modifiedSegment);
                    expressionFound = true;
                    CreateImage(entry.Value);
                    break;
                }
            }

            // Füge das ursprüngliche Segment zur Liste hinzu, wenn kein Ausdruck gefunden wurde
            if (!expressionFound)
            {
                modifiedSegments.Add(segment);
            }
        }

        // Setze den Text mit den modifizierten Segmenten zusammen
        string newText = string.Join("", modifiedSegments.ToArray());
        textComponent.text = newText;
    }

    int GetWordCount(string segment)
    {
        string[] words = segment.Split(' ');
        if (words.Length >= 2 && int.TryParse(words[0], out int count))
        {
            return count;
        }
        return 0;
    }

    void CreateImage(Sprite sprite)
    {
        // Erstelle ein neues GameObject mit dem Image-Präfab
        GameObject imageObject = Instantiate(imagePrefab, transform);

        // Positioniere das Bild an der Position des Textes
        RectTransform textTransform = textComponent.GetComponent<RectTransform>();
        RectTransform imageTransform = imageObject.GetComponent<RectTransform>();
        imageTransform.SetParent(textTransform.parent, false);
        imageTransform.anchoredPosition = textTransform.anchoredPosition;
        //imageTransform.sizeDelta = textTransform.sizeDelta/2;

        // Füge das Sprite zum Image-Komponenten hinzu
        Image imageComponent = imageObject.GetComponent<Image>();
        imageComponent.sprite = sprite;
        imageComponent.enabled = true;

        // Deaktiviere den Textkomponenten
        //textComponent.enabled = false;
    }*/
}

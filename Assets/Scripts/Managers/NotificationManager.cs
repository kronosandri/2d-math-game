using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    private static NotificationManager _instance;
    public static NotificationManager Instance 
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<NotificationManager>();
            }
            return _instance;
        }
    }

    [Header("UI Components")]
    public GameObject notificationPanel;
    public TextMeshProUGUI messageText;
    public Image iconImage;

    [Header("Settings")]
    public float displayTime = 3f;
    public Color formulaColor = Color.cyan;
    public Color systemColor = Color.white;

    void Awake()
    {
        
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        
        if (notificationPanel != null) notificationPanel.SetActive(false);
    }


    public void PopNotification(string message, Sprite icon = null, Color? textColor = null)
    {
        // Safety check to ensure we don't crash if the UI isn't assigned
        if (notificationPanel == null) return;

        StopAllCoroutines();
        
        messageText.text = message;
        messageText.color = textColor ?? systemColor;

        if (icon != null)
        {
            iconImage.sprite = icon;
            iconImage.gameObject.SetActive(true);
        }
        else
        {
            iconImage.gameObject.SetActive(false);
        }

        StartCoroutine(DisplayRoutine());
    }

    private IEnumerator DisplayRoutine()
    {
        notificationPanel.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        notificationPanel.SetActive(false);
    }
}
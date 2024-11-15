using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject connectedText;
    [SerializeField] private GameObject readyText;
    [SerializeField] private Button readyButton;

    public void ShowReadyUI()
    {
        readyText.SetActive(true);
        readyButton.gameObject.SetActive(false);
        connectedText.SetActive(false);
        Debug.Log("Updated UI to show ready state.");
    }

    public void ShowConnectedUI()
    {
        connectedText.SetActive(true);
        readyButton.gameObject.SetActive(true);
        readyText.SetActive(false);
        Debug.Log("Updated UI to show connected state.");
    }
}

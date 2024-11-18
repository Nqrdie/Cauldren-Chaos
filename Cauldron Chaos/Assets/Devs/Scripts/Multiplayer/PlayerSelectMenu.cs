using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject connectedText;

    public void ShowReadyUI()
    {
        connectedText.SetActive(false);
    }

    public void ShowConnectedUI()
    {
        connectedText.SetActive(true);
    }
}

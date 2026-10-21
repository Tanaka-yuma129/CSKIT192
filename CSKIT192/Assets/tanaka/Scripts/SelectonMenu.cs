using UnityEngine;

public class SelectionMenu : MonoBehaviour
{
    public GameObject selectionPanel;
    public GameObject flightPanel;
    public GameObject deliveryPersonPanel;
    public GameObject factoryPanel;
    public GameObject[] flightObjects;
    public GameObject[] selectionButtons;
    public GameObject flyButton;
    public PlayerFlight playerFlight;
    public bool IsFlightScreen { get; private set; }
    void Start()
    {
        ShowSelection();
    }
    // 最初のメニュー画面
    public void ShowSelection()
    {
        IsFlightScreen = false;
        if (playerFlight != null)
            playerFlight.SetCanAim(false);
        if (selectionPanel != null)
            selectionPanel.SetActive(true);
        if (flightPanel != null)
            flightPanel.SetActive(false);
        if (deliveryPersonPanel != null)
            deliveryPersonPanel.SetActive(false);
        if (factoryPanel != null)
            factoryPanel.SetActive(false);
        if (flyButton != null)
            flyButton.SetActive(true);
        SetFlightObjects(false);
        SetSelectionButtons(true);
    }
    // 飛行画面
    public void ShowFlight()
    {
        IsFlightScreen = true;
        if (selectionPanel != null)
            selectionPanel.SetActive(false);
        if (flightPanel != null)
            flightPanel.SetActive(true);
        if (deliveryPersonPanel != null)
            deliveryPersonPanel.SetActive(false);
        if (factoryPanel != null)
            factoryPanel.SetActive(false);
        if (playerFlight != null)
        {
            playerFlight.enabled = true;
            playerFlight.SetCanAim(true);
        }
        if (flyButton != null)
            flyButton.SetActive(false);
        SetFlightObjects(true);
        SetSelectionButtons(false);
    }
    // 配達員選択画面
    public void ShowDeliveryPerson()
    {
        IsFlightScreen = false;
        if (playerFlight != null)
            playerFlight.SetCanAim(false);
        if (selectionPanel != null)
            selectionPanel.SetActive(false);
        if (flightPanel != null)
            flightPanel.SetActive(false);
        if (deliveryPersonPanel != null)
            deliveryPersonPanel.SetActive(true);
        if (factoryPanel != null)
            factoryPanel.SetActive(false);
        SetFlightObjects(false);
        SetSelectionButtons(false);
    }
    // 工場選択画面
    public void ShowFactory()
    {
        IsFlightScreen = false;
        if (playerFlight != null)
            playerFlight.SetCanAim(false);
        if (selectionPanel != null)
            selectionPanel.SetActive(false);
        if (flightPanel != null)
            flightPanel.SetActive(false);
        if (deliveryPersonPanel != null)
            deliveryPersonPanel.SetActive(false);
        if (factoryPanel != null)
            factoryPanel.SetActive(true);
        SetFlightObjects(false);
        SetSelectionButtons(false);
    }
    // 飛行に使うオブジェクトの表示・非表示
    void SetFlightObjects(bool visible)
    {
        if (flightObjects == null) return;
        foreach (GameObject obj in flightObjects)
        {
            if (obj != null)
                obj.SetActive(visible);
        }
    }
    void SetSelectionButtons(bool visible)
    {
        if (selectionButtons == null) return;
        foreach (GameObject obj in selectionButtons)
        {
            if (obj != null)
                obj.SetActive(visible);
        }
    }
}
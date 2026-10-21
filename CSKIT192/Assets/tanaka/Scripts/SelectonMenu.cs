using UnityEngine;

public class SelectionMenu : MonoBehaviour
{
    public GameObject selectionPanel;
    public GameObject flightPanel;
    public GameObject deliveryPersonPanel;
    public GameObject factoryPanel;
    public GameObject[] flightObjects;
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
        SetFlightObjects(false);
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
        SetFlightObjects(true);
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
}
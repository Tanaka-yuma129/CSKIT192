using UnityEngine;

public class SelectionMenu : MonoBehaviour
{
    public GameObject selectionPanel;
    public GameObject flightPanel;
    public GameObject deliveryPersonPanel;
    public GameObject factoryPanel;
    public GameObject deliveryItemPanel;
    public GameObject earthObject;
    public GameObject deliveryPersonObject;
    public GameObject[] flightObjects;
    public GameObject[] selectionButtons;
    public GameObject returnToEarthButton;
    public GameObject flyButton;
    public GameObject selectedItemTextObject;
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

        if (deliveryItemPanel != null)
            deliveryItemPanel.SetActive(false);
        if (earthObject != null)
            earthObject.SetActive(true);
        if (deliveryPersonObject != null)
            deliveryPersonObject.SetActive(true);
        if (selectedItemTextObject != null)
            selectedItemTextObject.SetActive(false);
        if (returnToEarthButton != null)
            returnToEarthButton.SetActive(false);


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
        if (selectedItemTextObject != null)
            selectedItemTextObject.SetActive(true);
        if (returnToEarthButton != null)
            returnToEarthButton.SetActive(true);


        SetFlightObjects(true);
        SetSelectionButtons(false);
    }
    // 工場選択画面
    public void ShowFactory()
    {
        IsFlightScreen = false;
        if (selectedItemTextObject != null)
            selectedItemTextObject.SetActive(false);
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
    // 配達物選択画面
    public void ShowDeliveryItem()
    {
        IsFlightScreen = false;
        if (selectedItemTextObject != null)
            selectedItemTextObject.SetActive(false);
        if (playerFlight != null)
            playerFlight.SetCanAim(false);
        if (selectionPanel != null)
            selectionPanel.SetActive(false);
        if (flightPanel != null)
            flightPanel.SetActive(false);
        if (deliveryPersonPanel != null)
            deliveryPersonPanel.SetActive(false);
        if (factoryPanel != null)
            factoryPanel.SetActive(false);
        if (deliveryItemPanel != null)
            deliveryItemPanel.SetActive(true);
        if (earthObject != null)
            earthObject.SetActive(false);
        if (deliveryPersonObject != null)
            deliveryPersonObject.SetActive(false);
        if (flyButton != null)
            flyButton.SetActive(false);
        SetFlightObjects(false);
        SetSelectionButtons(false);
        if (deliveryItemPanel != null)

            deliveryItemPanel.SetActive(true);

        if (deliveryItemPanel != null)

        {

            Transform backButton =

                deliveryItemPanel.transform.Find("BackToEarthButton");

            if (backButton != null)

                backButton.gameObject.SetActive(true);

        }

    }
    // 地球画面に戻る
    public void ReturnToEarthScreen()
    {
        ShowSelection();
        if (deliveryItemPanel != null)
            deliveryItemPanel.SetActive(false);
        if (earthObject != null)
            earthObject.SetActive(true);
        if (deliveryPersonObject != null)
            deliveryPersonObject.SetActive(true);
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
    // 配達員が飛び立ったら戻るボタンを消す
    public void HideReturnButton()
    {
        if (returnToEarthButton != null)
            returnToEarthButton.SetActive(false);
    }
}
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public SelectionMenu selectionMenu;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        // ”òs‰æ–ÊˆÈŠO‚Å‚ÍƒJƒƒ‰‚ğ“®‚©‚³‚È‚¢
        if (selectionMenu == null ||
            !selectionMenu.IsFlightScreen)
        {
            return;
        }
        float move = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            move = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
        }
        transform.position +=
            Vector3.right * move * moveSpeed * Time.deltaTime;
    }
}

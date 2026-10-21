using UnityEngine;

public class KY_Camera : MonoBehaviour    
{
    [SerializeField] public float MoveSpeed = 0.1f;
 
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");


        Vector3 moveDelta = new Vector3(h, 0, 0) * MoveSpeed;
        transform.Translate(moveDelta);
    }
}

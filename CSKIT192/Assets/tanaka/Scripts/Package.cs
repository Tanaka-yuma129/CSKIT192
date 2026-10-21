using UnityEngine;
public class Package : MonoBehaviour
{
    void Update()
    {
        // ‰æ–Ê‚Ì‚©‚È‚è‰º‚Ü‚Å—‚¿‚½‚çÁ‚·
        if (transform.position.y < -15f)
        {
            Destroy(gameObject);
        }
    }
}
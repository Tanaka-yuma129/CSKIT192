using UnityEngine;
public class DestroyDroppedItem : MonoBehaviour
{
    // “Ş—‚Æ‚İ‚È‚·YÀ•W
    public float fallLimitY = -15f;
    void Update()
    {
        // ˆê’è‚Ì‚‚³‚æ‚è‰º‚É—‚¿‚½‚çÁ‚·
        if (transform.position.y < fallLimitY)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ’n‹…‚â˜f¯‚ÉG‚ê‚½‚çÁ‚·
        if (collision.gameObject.CompareTag("Earth") ||
            collision.gameObject.CompareTag("Planet"))
        {
            Destroy(gameObject);
        }
    }
}
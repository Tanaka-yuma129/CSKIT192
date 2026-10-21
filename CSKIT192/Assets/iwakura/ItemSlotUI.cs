using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemSlotUI : MonoBehaviour
{
    [System.Serializable]
    public struct ItemSlot
    {
        public Image iconImage;       // アイコンのImage
        public TextMeshProUGUI countText; // 個数を表示するテキスト
        [HideInInspector] public int count; // 現在の個数
    }
    [Header("3つのアイテムスロット")]
    public ItemSlot[] slots = new ItemSlot[3];
    void Start()
    {
        UpdateUI();
    }
    // 指定したスロットのアイテムを増やす（例: index 0 = トマト）
    public void AddItem(int slotIndex, int amount = 1)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            slots[slotIndex].count += amount;
            UpdateUI();
        }
    }
    // 指定したスロットのアイテムを減らす
    public void RemoveItem(int slotIndex, int amount = 1)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            slots[slotIndex].count -= amount;
            if (slots[slotIndex].count < 0) slots[slotIndex].count = 0;
            UpdateUI();
        }
    }
    // 画面の表示を更新する
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 個数が0個より多ければ表示、0個なら半透明にするなど
            if (slots[i].count > 0)
            {
                // アイコンをはっきり表示
                if (slots[i].iconImage != null)
                {
                    Color c = slots[i].iconImage.color;
                    c.a = 1.0f;
                    slots[i].iconImage.color = c;
                }
                // 個数テキストを表示
                if (slots[i].countText != null)
                {
                    slots[i].countText.text = "x " + slots[i].count;
                    slots[i].countText.gameObject.SetActive(true);
                }
            }
            else
            {
                // 持っていないときはアイコンを薄くし、テキストを非表示にする
                if (slots[i].iconImage != null)
                {
                    Color c = slots[i].iconImage.color;
                    c.a = 0.2f;
                    slots[i].iconImage.color = c;
                }
                if (slots[i].countText != null)
                {
                    slots[i].countText.text = "";
                    // slots[i].countText.gameObject.SetActive(false); // 0個のときに文字を消したい場合はコメントアウトを外してください
                }
            }
        }
    }
}



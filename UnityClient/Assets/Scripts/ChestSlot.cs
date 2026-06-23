using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    public string type;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(quantityText.text=="0")
        {
            SetEmpty();
        }
    }
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
        type = "";
    }
    public void SetSlot(Sprite sprite,int quantity, string type)
    {
        itemIcon.sprite = sprite;
        itemIcon.color = new Color(1, 1, 1, 1);
        quantityText.text = quantity.ToString();
        this.type = type;
    }
    public void RemoveItem()
    {
        string str = quantityText.text;
        if (int.TryParse(str, out int number))
        {
            number--;
            quantityText.text = number.ToString();
        }

    }
    public void FromChestToInventory()
    {
        GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
        AudioManager aum = audiomen.GetComponent<AudioManager>();
        aum.PlaySoundEffect(aum.slotSound);
        if (type!="")
        {
            Player playerScript=player.GetComponent<Player>();
            if(playerScript.CanAddItem(type))
            {
                playerScript.inventory.AddSlot(type, itemIcon.sprite);
                RemoveItem();
            }
        }
    }
}

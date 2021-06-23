using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public int value;

    private ItemManager itemManager;
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = FindObjectOfType<ItemManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag.Equals("Player"))
        {
            itemManager.AddItem(this.value);
            playerManager.ShowUFXPickUpItem(this.transform);

            Destroy(this.gameObject);
        }
    }
}

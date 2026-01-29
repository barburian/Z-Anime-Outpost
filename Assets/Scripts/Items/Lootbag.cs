// FILE: Assets/Scripts/Components/LootBag.cs
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [Header("Config")]
    public GameObject itemPrefab;
    public List<ItemData> possibleLoot; // Drag your ScriptableObjects (Spinach, Armor, etc.) here
    
    [Range(0f, 100f)]
    public float dropChance = 100f; // 25% chance to drop

    public void TryDropLoot()
    {
        // 1. Roll the dice
        float roll = Random.Range(0f, 100f);
        if (roll <= dropChance && possibleLoot.Count > 0)
        {
            // 2. Pick a random item from the list
            int randomIndex = Random.Range(0, possibleLoot.Count);
            ItemData selectedItem = possibleLoot[randomIndex];

            // 3. Spawn the physical prefab
            GameObject droppedItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);

            // 4. Inject the data into the spawned object
            ItemPickup pickupScript = droppedItem.GetComponent<ItemPickup>();
            if (pickupScript != null)
            {   Debug.Log("a picat itemul");
                pickupScript.itemData = selectedItem;
            }
        }
    }
}
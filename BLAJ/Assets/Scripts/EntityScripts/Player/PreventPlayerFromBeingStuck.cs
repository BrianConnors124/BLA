using UnityEngine;
using UnityEngine.Tilemaps;

public class PreventPlayerFromBeingStuck : MonoBehaviour
{
    public TilemapCollider2D tileMapCollider;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tileMapCollider.excludeLayers += LayerMask.GetMask("Player");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tileMapCollider.excludeLayers -= LayerMask.GetMask("Player");
        }
    }
}

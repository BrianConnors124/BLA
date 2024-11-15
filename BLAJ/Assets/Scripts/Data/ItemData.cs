using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    public string name;
    public string GUID;
    public Sprite icon;
    public int stackSize;

    public void Attack()
    {
        Debug.Log("yo");
    }
}

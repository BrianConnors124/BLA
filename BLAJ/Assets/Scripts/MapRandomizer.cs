
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    [Header("Location And Direction")] 
    [SerializeField] private Vector3 _location;
    [SerializeField] private Vector2 _direction;

    [Header("GameObject Inst")] 
    [SerializeField] private GameObject[] platform;
    public bool placeable;


    private void Awake()
    {
        _location = transform.position;
        StartCoroutine(TryPlace());
    }

    private IEnumerator TryPlace()
    {

        for (var i = 0; i < platform.Length; i++)
        {
            var rand = Random.Range(0, 1);
            _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            BoxCollider2D platformCol = platform[i].GetComponent<BoxCollider2D>();
            Vector2 colSize = platformCol.size;
            Vector2 scaleSize = platform[i].transform.localScale;
            Instantiate(platform[i], new Vector2((float) _location.x + _direction.x * colSize.x * scaleSize.x, (float) _location.y + _direction.y * colSize.y * scaleSize.y), Quaternion.identity);
            Debug.Log(placeable);
            yield return new WaitForSeconds(1);
        }
    }
}

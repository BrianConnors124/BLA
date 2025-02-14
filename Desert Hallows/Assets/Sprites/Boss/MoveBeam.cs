using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeam : MonoBehaviour
{
    public BeamDream beam;
    public GameObject player;
    


    private void Start()
    {
        player = GameObject.Find("Player");
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        beam.Beamy(new Vector3(-23.52f, 0, 0));
    }

    private void Update()
    {
        var Dir = (player.transform.position - transform.position);
        var AngleR = Mathf.Atan2(Dir.y, Dir.x);
        var AngleD = AngleR * Mathf.Rad2Deg;

        var targetRotation = Quaternion.Euler(0, 0, AngleD);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoncLookAtPlayer : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]private float _turnSpeed = 10f;
    private Vector3 _lookAtPlayer;
    private void Start()
    {
        _player = GameManagement.GetPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var lookPos = _player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 15);

        // transform.LookAt(_lookAtPlayer);

        // transform.rotation = Quaternion.Slerp(transform.rotation,
        //   Quaternion.LookRotation(_player.transform.position - transform.position),
        //  _turnSpeed * Time.deltaTime);    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerUI : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        if (player)
        {
            transform.LookAt(player);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
        }
    }
}

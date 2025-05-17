using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] private Transform restartPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathPanel;

    public void OnRestartButton()
    {
        print(player.transform.position);
        print(restartPoint.position);

        player.transform.position = restartPoint.position;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathPanel.SetActive(false);
    }
    
    
}

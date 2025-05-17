using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPasueMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Vendor vendor;
    
    private bool isOpen = false;
    public bool GetStatus() => isOpen;
    private void Start()
    {
        pausePanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
           Pause();
        }
    }
    private void Pause()
    {
        
        if (vendor.GetStatus())
            return;
        isOpen = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Resume()
    {
        isOpen = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    public void OnResumeButton() => Resume();

    public void OnExitButton()
    {
        print("Exit");
        Application.Quit();
    }
}

using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI; 
    [SerializeField] private GameObject shopUI;

    [SerializeField] private OpenPasueMenu openPasueMenu;
    
    private bool isPlayerInRange = false;
    private bool isOpen = false;
    
    public bool GetStatus() => isOpen;
    private void Start()
    {
        interactionUI.SetActive(false);
        shopUI.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionUI.SetActive(false);
            shopUI.SetActive(false);
        }
    }

    public void ToggleShop()
    {
        if (isOpen)
        {
            shopUI.SetActive(false);
            Time.timeScale = 1;
            interactionUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
        else
        {   
            if (openPasueMenu.GetStatus())
                return;
            shopUI.SetActive(true);
            Time.timeScale = 0;
            interactionUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        isOpen = !isOpen;
    }
}
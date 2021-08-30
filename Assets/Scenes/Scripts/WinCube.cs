using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCube : MonoBehaviour
{
    [SerializeField] Canvas WinCanvas = null;
    private void Start()
    {
        WinCanvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WinCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            FindObjectOfType<WeaponSwitch>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

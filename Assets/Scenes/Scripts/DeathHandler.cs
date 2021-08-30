using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas = null;
    private void Start() {
        gameOverCanvas.gameObject.SetActive(false);
    }
    public void HandleDeath() {
        gameOverCanvas.gameObject.SetActive(true);
        print("DEAD");
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitch>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

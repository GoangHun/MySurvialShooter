using UnityEngine;


public class PlayerShooter : MonoBehaviour {
    public Gun gun; 

    private PlayerInput playerInput;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable() {
        // 슈터가 활성화될 때 총도 함께 활성화
        gun.gameObject.SetActive(true);
    }
    
    private void OnDisable() {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        gun.gameObject.SetActive(false);
    }

    private void Update() {
        if (playerInput.fire)
        {
			gun.Fire();
		}  
    }

  
}
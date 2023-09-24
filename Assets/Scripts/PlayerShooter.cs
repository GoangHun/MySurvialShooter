using UnityEngine;


public class PlayerShooter : MonoBehaviour {
    public Gun gun;
    public ParticleSystem ring;
    public LayerMask whatIsTarget;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ring.isPlaying)
            {
                ring.Stop();
            }
            else
            {
                ring.Play();
            }
            
        }

        if (ring.isPlaying)
        {
            Collider[] collides =
                    Physics.OverlapSphere(transform.position, 10f, whatIsTarget);

            for (int i = 0; i < collides.Length; i++)
            {
                var enemy = collides[i].GetComponent<Enemy>();
                enemy.OnDamage(2f, Vector3.zero, Vector3.zero);
            }
        }

    }

  
}
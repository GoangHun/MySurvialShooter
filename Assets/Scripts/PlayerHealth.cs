using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : LivingEntity {
    public Slider healthSlider; 
    private Quaternion healthSliderRot;

    public AudioClip deathClip;
    public AudioClip hitClip; 

    private AudioSource playerAudioPlayer; 
    private Animator playerAnimator; 

    private PlayerMovement playerMovement; 
    private PlayerShooter playerShooter;

    private void Awake() {

		playerAudioPlayer = GetComponent<AudioSource>();
		playerAnimator = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		playerShooter = GetComponent<PlayerShooter>();

	}

    protected override void OnEnable() {
        base.OnEnable();

        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

	// 체력 회복
	public override void RestoreHealth(float newHealth) { 
        base.RestoreHealth(newHealth);
    }

    // 데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection) {
        
        if (!dead)
        {
			playerAudioPlayer.PlayOneShot(hitClip);
		}
        
        base.OnDamage(damage, hitPoint, hitDirection);
        StartCoroutine(UIManager.instance.OnDamageEffect());
        UIManager.instance.UpdateHpUI(health / startingHealth);

    }

    public override void Die() {
        base.Die();

        playerAudioPlayer.PlayOneShot(deathClip);
        playerAnimator.SetTrigger("Die");
		playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

}
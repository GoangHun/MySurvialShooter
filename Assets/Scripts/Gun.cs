using System.Collections;
using UnityEngine;

// 총을 구현한다
public class Gun : MonoBehaviour {

    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리

    public float damage = 25; // 공격력
    private float fireDistance = 50f; // 사정거리

    public float timeBetFire = 0.12f; // 총알 발사 간격
    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake() {
        // 사용할 컴포넌트들의 참조를 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;   //직선만 그리기
        bulletLineRenderer.enabled = false;     //on/off
    }

    private void OnEnable() {
        // 총 상태 초기화  
        lastFireTime = 0f;
    }


	// 발사 시도
	public void Fire() {
        if (Time.time > lastFireTime + timeBetFire)
        {
			lastFireTime = Time.time;
			Shot();
        }

    }

    // 실제 발사 처리
    private void Shot() {
        var hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
		var ray = new Ray(fireTransform.position, fireTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, fireDistance)) //(디테일)좀비의 박스 콜라이더에 맞지 않게 할 필요가 있음
		{
            var target = hit.collider.GetComponent<IDamageable>();   //IDamageable을 상속 받은 클래스를 반환함
            if (target != null) 
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
		}
        StartCoroutine(ShotEffect(hitPosition));
     
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition) {   //코루틴을 사용해서 동작

        muzzleFlashEffect.Play();
        //gunAudioPlayer.PlayOneShot(shotClip);

		// 라인 렌더러를 활성화하여 총알 궤적을 그린다

		bulletLineRenderer.SetPosition(0, fireTransform.position);
		bulletLineRenderer.SetPosition(1, hitPosition);
		bulletLineRenderer.enabled = true;
        

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }
}
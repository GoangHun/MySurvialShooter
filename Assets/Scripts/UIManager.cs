using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>(); //모든 오브젝트를 순회하기 때문에 사용을 지양해야 함
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public TextMeshProUGUI scoreText; // 점수 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public GameObject playerOnDamageEffet;
    public GameObject panelGUI;
    public Image hpFill;
    public Slider musicSlider;
    public Slider effectSlider;
    public Toggle soundOfOff;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(GameManager.instance.UpdateMusicVolume);
        effectSlider.onValueChanged.AddListener(GameManager.instance.UpdateEffectVolume);
        soundOfOff.onValueChanged.AddListener(GameManager.instance.SoundOnOff);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = panelGUI.activeSelf ? 1f : 0f;
            panelGUI.SetActive(!panelGUI.activeSelf);
            GameManager.instance.isPause = panelGUI.activeSelf;
        }

        if (GameManager.instance.isGameover && Input.anyKey)
        {
            GameManager.instance.SceneLoad();
        }
    }

    public IEnumerator OnDamageEffect()
    {
        playerOnDamageEffet.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        playerOnDamageEffet.SetActive(false);

    }

    public void UpdateHpUI(float hp)
    {
        hpFill.fillAmount = hp;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore) {
        scoreText.text = "Score : " + newScore;
    }

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(active);
    }
}
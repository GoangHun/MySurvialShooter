using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    private int score = 0; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버 상태
    public bool isPause { get; set; }

    public AudioClip musicClip;
    private AudioSource gmAudioPlayer;
    public List<AudioSource> effectSources;

    private void Awake() {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        gmAudioPlayer = GetComponent<AudioSource>();
    }

    private void Start() {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
        gmAudioPlayer.loop = true;
        gmAudioPlayer.playOnAwake = true;
        gmAudioPlayer.clip = musicClip;
        gmAudioPlayer.Play();
    }

    public void AddScore(int newScore) {
        if (!isGameover)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);
        }
    }

    public void EndGame() {
        isGameover = true;
        UIManager.instance.SetActiveGameoverUI(true);
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR    //유니티 에디터에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else   //빌드된 에플리케이션 종료
    Application.Quit();     
#endif
    }

    public void UpdateMusicVolume(float volume)
    {
        gmAudioPlayer.volume = volume; 
    }

    public void UpdateEffectVolume(float volume)
    {
        foreach (AudioSource player in effectSources)
        {
            player.volume = volume;
        }
    }

    public void SoundOnOff(bool mute)
    {
        gmAudioPlayer.mute = !mute;
        foreach (AudioSource player in effectSources)
        {
            player.mute = !mute;
        }
    }
}
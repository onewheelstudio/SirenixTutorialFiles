using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    [BoxGroup("Game State Info")]
    [EnumToggleButtons]
    [OnValueChanged("StateChange")]
    [ShowInInspector]
    public static GameState gameState;

    [BoxGroup("Game State Info")]
    [ShowInInspector]
    public static int turnsRemaining = 3;

    //UI elements
    [TabGroup("UI")]
    [SceneObjectsOnly]
    [Required]
    [InlineButton("SelectCanvas","Select")]
    public Canvas startButtons;
    [TabGroup("UI")]
    [SceneObjectsOnly,Required]
    [InlineButton("SelectCanvas","Select")]
    public Canvas pauseMenu;
    [TabGroup("UI")]
    [SceneObjectsOnly,Required]
    [InlineButton("SelectCanvas","Select")]
    public Canvas HUD;

    //background music
    [TabGroup("Music")]
    public AudioSource musicSource;

    [Space]
    [ShowInInspector]
    [ValueDropdown("musicList")]
    [TabGroup("Music")]
    [InlineButton("PlayMusic")]
    private AudioClip currentMusicClip;
    [TabGroup("Music")]
    [InlineEditor(InlineEditorModes.SmallPreview)]
    public List<AudioClip> musicList;

    //sfx
    [TabGroup("SFX")]
    public AudioSource sfxSource;
    [TabGroup("SFX")]
    [InlineButton("PlaySFX","Test")]
    public AudioClip uiClick;
    [TabGroup("SFX")]
    [InlineButton("PlaySFX","Test")]
    public AudioClip weaponShoot;
    [TabGroup("SFX")]
    [InlineButton("PlaySFX","Test")]
    public AudioClip weaponHit;
    [TabGroup("SFX")]
    [InlineButton("PlaySFX","Test")]
    public AudioClip enemySpawn;

    //enemies
    [TabGroup("Enemies", "Enemy Data")]
    [AssetsOnly]
    public GameObject enemyPrefab;
    [TabGroup("Enemies", "Enemy Data")]
    [InlineEditor(InlineEditorModes.GUIOnly)]
    [AssetsOnly]
    public List<EnemyData> enemyList;

    //spawn points
    [TabGroup("Enemies","Spawn Points")]
    [SceneObjectsOnly]
    public List<Transform> spawnPoints;

    private void PlaySFX(AudioClip sfx)
    {
        if (sfxSource != null && !sfxSource.isPlaying)
                sfxSource.PlayOneShot(sfx);
    }

    public void PlayMusic(AudioClip music)
    {
        if (musicSource != null && music != null)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    [Button(ButtonSizes.Medium)]
    [TabGroup("Enemies","Enemy Data")]
    [GUIColor(0.6f,1f,0.6f)]
    public void SpawnRandomEnemy()
    {
        if (enemyList.Count == 0 || spawnPoints.Count == 0)
            return;

        GameObject enemyToSpawn = Instantiate(enemyPrefab);

        //inject data
        EnemyData data = enemyList[Random.Range(0, enemyList.Count)];
        enemyToSpawn.GetComponent<EnemyControl>().SetEnemyData(data);

        //set location
        enemyToSpawn.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
    }

    public void StateChange()
    {
        switch (gameState)
        {
            case GameState.startScene:
                break;
            case GameState.gamePlay:
                break;
            case GameState.paused:
                break;
            case GameState.complete:
                break;
            default:
                break;
        }
    }

    private void SelectCanvas(Canvas _object) 
    {
        if(_object)
            UnityEditor.Selection.activeObject = _object.gameObject;
    }
}

public enum GameState
{
    startScene,
    gamePlay,
    paused,
    complete
}

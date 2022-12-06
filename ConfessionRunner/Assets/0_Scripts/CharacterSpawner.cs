using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    [SerializeField] GameObject[] parentPrefabs;
    public Transform spawnPoint, parentSpawnPoint;
    public int selectedCharacter;
    public GameObject clone, settingsUI;
    public float minX,maxX;

    public static CharacterSpawner Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];
        clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<SwerveMovementSystem>().minX = this.minX;
        clone.GetComponent<SwerveMovementSystem>().maxX = this.maxX;
        Time.timeScale = 0;

    }

    public void Game2Settings()
    {
        settingsUI.SetActive(true);
    }
    public void Settings2Game()
    {
        settingsUI.SetActive(false);
    }
    public void CharStoreGame()
    {
        SceneManager.LoadScene("CharSelectedScene");
    }

}

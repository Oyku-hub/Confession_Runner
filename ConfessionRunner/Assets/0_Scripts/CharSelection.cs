using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelection : MonoBehaviour
{
    public ParticleSystem _smoke;
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public bool isGameActive;
    public static CharSelection Instance { get; private set; }

    public void Start()
    {
        Time.timeScale = 1;
        ParticleSystem _smoke = GameObject.Find("Smoke").gameObject.GetComponent<ParticleSystem>(); ///not tested
        NextCharacter();
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        isGameActive = false;
    }
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        _smoke.Play();
        StartCoroutine(WaitSmoke());
        isGameActive = true;

    }

    IEnumerator WaitSmoke()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene(2, LoadSceneMode.Single);

        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}

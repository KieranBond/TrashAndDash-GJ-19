using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    GameObject evnAssets;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = evnAssets.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        evnAssets.transform.position = new Vector3(0,1,0) * Mathf.Sin(Time.time) * 0.5f;
    }

    public void PlayGame()
    {
        evnAssets.SetActive(false);
        gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

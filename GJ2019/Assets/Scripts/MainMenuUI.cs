using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject gameAssets;
    [SerializeField]
    GameObject evnAssets;

    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private float m_fadetime = 1f;

    [SerializeField]
    private Button m_playButton;

    [SerializeField]
    private Button m_quitButton;


    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = evnAssets.transform.position;

        m_canvasGroup.interactable = true;
        m_playButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        evnAssets.transform.position = new Vector3(0,1,0) * Mathf.Sin(Time.time) * 0.5f;

    }

    public void PlayGame()
    {
        evnAssets.SetActive(false);
        gameAssets.SetActive(true);

        m_canvasGroup.interactable = false;

        StartCoroutine(DoFade());
    }

    private IEnumerator DoFade()
    {
        float step = 1;

        do
        {
            step -= Time.deltaTime / m_fadetime;
            m_canvasGroup.alpha = step;
            yield return null;
        }
        while (m_canvasGroup.alpha > 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

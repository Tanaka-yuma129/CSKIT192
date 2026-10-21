using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private AudioSource Enter;//AudioSourceŒ^‚Ì•Ï”Enter‚ğéŒ¾
    [SerializeField] private AudioClip BGM;//AudioClipŒ^‚Ì•Ï”SE‚ğéŒ¾
    [SerializeField] private AudioClip SE;//AudioClipŒ^‚Ì•Ï”SE‚ğéŒ¾
    [SerializeField] private float SEtime = 2.0f;
    private bool isTransitioning = false;

    private void Start()
    {
        Enter.PlayOneShot(BGM);//AudioClipŒ^‚Ì•Ï”BGM‚ğˆø”‚Æ‚µ‚ÄAAudioSourceŒ^‚ª“ü‚Á‚Ä‚¢‚é•Ï”Enter‚ğÄ¶
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(TransitionSequence());
        }

    }

    private IEnumerator TransitionSequence()
    {
        isTransitioning = true;

        Enter.PlayOneShot(SE);//AudioClipŒ^‚Ì•Ï”SE‚ğˆø”‚Æ‚µ‚ÄAAudioSourceŒ^‚ª“ü‚Á‚Ä‚¢‚é•Ï”Enter‚ğÄ¶

        yield return new WaitForSeconds(SEtime);

        SceneManager.LoadScene("MainScene"); //ƒV[ƒ“w’è:MainScene
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public AudioClip runSound;
    public AudioClip exitSound;
    public AudioClip clankSound;
    public AudioSource mainMusicHolder;


    private Animator anim;
    public Animator fadeAnim;
    public Conversation introConvo;
    public Conversation slide2Convo;
    public Conversation slide3Convo;
    public Conversation finalConvo;
    public GameObject[] slides;
    public GameObject nextButton;
    public GameObject noConvoButton;
    private int curSlide;
    private bool shake;
    private bool newConvo;
    private bool hideButton;
    private bool loadNextLevel;

    void Start()
    {
        hideButton = true;
        anim = Camera.main.GetComponent<Animator>();
        StartCoroutine(Intro());

        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);
        }

        SelectSlide(0);
    }

    void Update()
    {
        if (hideButton)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }

        for (int i = 0; i < slides.Length; i++)
        {
            if(slides[1].activeInHierarchy == true && !newConvo)
            {
                StartCoroutine(Slide2());
                hideButton = true;
                newConvo = true;
            }
            else if(slides[2].activeInHierarchy == true && !newConvo)
            {
                StartCoroutine(Slide3());
                hideButton = true;
                newConvo = true;
            }
            else if(slides[3].activeInHierarchy == true && !newConvo)
            {
                StartCoroutine(SlideShow());
                hideButton = true;
                newConvo = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.Instance.LoadLevel("MainMenu");
        }
    }

    IEnumerator Intro()
    {
        fadeAnim.SetBool("Fade", false);
        anim.SetBool("Zoom", false);
        yield return new WaitForSeconds(3);
        CutsceneConvo(introConvo);
        shake = true;
    }

    public IEnumerator Slide2()
    {
        anim.SetBool("Pan", true);
        yield return new WaitForSeconds(.5f);
        CutsceneConvo(slide2Convo);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Slide3()
    {
        anim.SetBool("Pan", false);
        yield return new WaitForSeconds(.5f);
        CutsceneConvo(slide3Convo);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator SlideShow()
    {
        nextButton.SetActive(false);
        NextSlide();
        yield return new WaitForSeconds(2);
        NextSlide();
        yield return new WaitForSeconds(1);
        NextSlide();
        yield return new WaitForSeconds(2);
        NextSlide();
        mainMusicHolder.mute = true;
        AudioManager.Instance.PlayClip(clankSound);
        yield return new WaitForSeconds(2);      
        NextSlide();
        yield return new WaitForSeconds(2);
        NextSlide();
        AudioManager.Instance.PlayClip(runSound);
        yield return new WaitForSeconds(1);
        NextSlide();
        yield return new WaitForSeconds(1);
        FadeToBlack();
        yield return new WaitForSeconds(3.5f);
        CutsceneConvo(finalConvo);
        AudioManager.Instance.PlayClip(exitSound);
        loadNextLevel = true;
        
    }

    public IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(.5f);
        if (DialogueDisplay.Instance.inConvo)
        {
            hideButton = true;
        }
        else
        {
            hideButton = false;
        }
    }

    void CutsceneConvo(Conversation conversation)
    {
        DialogueDisplay.Instance.conversation = conversation;
        DialogueDisplay.Instance.SetSpeakers();
        DialogueDisplay.Instance.AdvanceConversation();
    }

    public void SelectSlide(int slideIndex)
    {
        if (curSlide >= 0)
        {
            slides[curSlide].SetActive(false);
        }

        curSlide = slideIndex;
        slides[slideIndex].SetActive(true);
    }

    public void NextSlide()
    {
        if (!loadNextLevel)
        {
            int nextSlideIndex = (curSlide + 1) % slides.Length;
            SelectSlide(nextSlideIndex);
        }
        else
        {
            LevelManager.Instance.LoadLevel("MainMenu");
        }
    }

    public void WaitForButton()
    {
        StartCoroutine(EnableButton());
    }

    public void NextConvo()
    {
        newConvo = false;
    }

    public void FadeToBlack()
    {
        fadeAnim.SetBool("Fade", true);
    }

    public void ShakeCamera()
    {
        if (shake)
        {
            Camera.main.GetComponent<CameraShake>().StartShake(1f, .5f);
            shake = false;
        }
    }
}

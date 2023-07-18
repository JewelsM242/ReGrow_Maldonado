using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxHandler : MonoBehaviour
{
    [Header("Style Stuff")]
    public Color VineColor;
    public Color TextColor;

    [Header("Info for Appear/Disappear")]
    public Vector2 BabySize;
    public Vector2 FullSize;
    public bool DisableAppear; // make it so box just appears at full size
    public float ExpandTime;
    public float TextFadeTime;

    // derived + internal values
    private Vector3 CurrentSize;
    private float TransitionPercent;
    private float ExpandRate;
    private float TextFadeRate;
    private float TextAlpha;

    [Header("Listen I simply didn't want to do this in code")]
    public GameObject ULCorner;
    public GameObject LRCorner;
    public GameObject NameText;
    public GameObject WritingArea;

    // GameObject derived values
    private Image ulImage;
    private Image lrImage;
    private TMP_Text speakerName;
    private TMP_Text message;

    // for writing to textbox
    private string goalText;
    private bool writingDone;
    private float writeRate;

    public bool visible;

    // Start is called before the first frame update
    void Start()
    {
        if(DisableAppear)
        {
            CurrentSize = FullSize;
            TransitionPercent = 1;
            TextAlpha = 1;
        }
        else
        {
            CurrentSize = BabySize;
            TransitionPercent = 0;
            TextAlpha = 0;
        }

        ExpandRate = 1 / ExpandTime;
        TextFadeRate = 1 / TextFadeTime;

        var name_trans = (RectTransform)NameText.transform;
        name_trans.sizeDelta = FullSize;

        var text_trans = (RectTransform)WritingArea.transform;
        text_trans.sizeDelta = FullSize;

        ulImage = ULCorner.GetComponent<Image>();
        lrImage = LRCorner.GetComponent<Image>();

        speakerName = NameText.GetComponent<TMP_Text>();
        message = WritingArea.GetComponent<TMP_Text>();

        visible = false;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        // handling position shifts
        CurrentSize = Vector2.Lerp(BabySize, FullSize, TransitionPercent);

        var corner_place = CurrentSize / 2;
        corner_place.x = -corner_place.x;
        ULCorner.transform.localPosition = corner_place;
        LRCorner.transform.localPosition = -corner_place;

        // handling alpha values
        // var tp_mod = (1.5f * TransitionPercent) - .5f; // use to delay fade in of vines y' = (1 + b)y - b
        // to delay by a certain percent of the runtime, d, set b = (d/1-d); d E [0, 1)
        // then can do a check to set the alpha to 0 if tp_mod is negative -> making a delay effect
        var temp = VineColor;
        temp.a = TransitionPercent;
        ulImage.color = temp;
        lrImage.color = temp;

        temp = TextColor;
        temp.a = TextAlpha;
        speakerName.color = temp;
        message.color = temp;
    }

    public void ChangeSpeaker(string newName)
    {
        speakerName.text = newName;
    }

    public void SetMessage(string txt)
    {
        message.text = txt;
    }

    public void SetGoalText(string txt)
    {
        goalText = txt;
    }

    public void StartWriting(float chars_per_sec)
    {
        message.text = "";
        writingDone = false;
        writeRate = chars_per_sec;
        StartCoroutine(WriteMessage());
    }

    public void WriteThis(string txt, float chars_per_sec)
    {
        SetGoalText(txt);
        StartWriting(chars_per_sec);
    }

    public void FinishWriting()
    {
        if (!writingDone)
        {
            StopCoroutine(WriteMessage());
            message.text = goalText;
            writingDone = true;
        }
    }

    public bool IsWritingDone()
    {
        return writingDone;
    }

    private IEnumerator WriteMessage()
    {
        while (!visible)
        {
            yield return Appear();
        }
        for (int i = 0; i < goalText.Length && !writingDone; i++)
        {
            message.text = goalText.Substring(0,i);

            yield return new WaitForSecondsRealtime(1/writeRate);
        }
        writingDone = true;
    }

    private IEnumerator BoundsAppear()
    {
        TransitionPercent = 0;
        while (TransitionPercent < 1)
        {
            TransitionPercent += ExpandRate * Time.deltaTime;
            if (TransitionPercent > 1)
            {
                TransitionPercent = 1;
            }
            yield return null;
        }
    }

    private IEnumerator TextAppear()
    {
        TextAlpha = 0;
        while (TextAlpha < 1)
        {
            TextAlpha += TextFadeRate * Time.deltaTime;
            if (TextAlpha > 1)
            {
                TextAlpha = 1;
            }
            yield return null;
        }
    }

    private IEnumerator ByeBounds()
    {
        while (TransitionPercent > 0)
        {
            TransitionPercent -= ExpandRate * Time.deltaTime;
            if (TransitionPercent < 0)
            {
                TransitionPercent = 0;
            }
            yield return null;
        }
    }

    private IEnumerator ByeText()
    {
        while (TextAlpha > 0)
        {
            TextAlpha -= TextFadeRate * Time.deltaTime;
            if (TextAlpha < 0)
            {
                TextAlpha = 0;
            }
            yield return null;
        }
    }

    private IEnumerator Appear()
    {
        yield return BoundsAppear();

        yield return TextAppear();

        visible = true;
    }

    private IEnumerator FadeOut()
    {
        yield return ByeText();

        yield return ByeBounds();

        visible = false;
    }

    public void FadeAway()
    {
        StartCoroutine(FadeOut());
    }
}

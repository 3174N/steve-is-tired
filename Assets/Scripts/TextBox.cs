using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    #region variables
    public GameObject canvas;
    public TextMeshProUGUI text;

    public TextItem[] texts;

    bool hasShown = false;

    float duration;

    Animator animator;
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hasShown)
            return;

        StartCoroutine(Display());

        hasShown = true;
    }

    IEnumerator Display()
    {
        foreach (TextItem textItem in texts)
        {
            canvas.SetActive(true);
            animator.SetTrigger("Show");

            text.text = textItem.text;
            text.color = textItem.color;

            yield return new WaitForSeconds(textItem.duration);

            animator.SetTrigger("Fade");
            yield return new WaitForSeconds(1f);
        }

        canvas.SetActive(false);
    }
}

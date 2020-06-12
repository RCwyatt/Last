using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessagePop : MonoBehaviour
{

    public Canvas sheet;
    public CanvasRenderer pageRender;
    public CanvasRenderer textRender;
    public Text textBox;
    private bool showing = false;
    public PlayerMovement pauser;

    // Start is called before the first frame update
    void Start()
    {
        pageRender.SetAlpha(0f);
        textRender.SetAlpha(0f);
        showing = false;
    }


    public void RenderSheet(string text)
    {
        showing = true;
        pageRender.SetAlpha(1f);
        textBox.text = text;
        textRender.SetAlpha(1f);

        pauser.PausePlayer(false);

        StartCoroutine("ShowMessage");

    }

    public void EndRender()
    {
        if (showing)
        {
            pageRender.SetAlpha(0f);
            textRender.SetAlpha(0f);
            showing = false;
            pauser.PausePlayer(true);
        }
    }

    private IEnumerator ShowMessage()
    {
        bool stop = false;
        do
        {
            
            stop = Input.GetKeyDown(KeyCode.Space);
            if (!stop)
            {
                yield return null;
            }
        } while (!stop);
        EndRender();
    }

}

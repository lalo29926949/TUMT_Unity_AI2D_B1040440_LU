using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    #region 欄位


    public enum state
    {
        start, notComplete, complete
    }
    public state _state;

    [Header("對話")]
    public string sayStart = "嗨!你好!我可以請你幫我蒐集十朵花嗎？";
    public string sayNotComplete = "你還沒找到十朵花喔...";
    public string sayComplete = "感謝你幫我找到十朵花~";
    [Header("對話速度")]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    #endregion

    public AudioClip soundSay;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }


    // 2D 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到物件為"騎士"
        if (collision.name == "騎士")
            Say();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "騎士")
            SayClose();
    }


    /// <summary>
    /// 對話：打字效果
    /// </summary>
    private void Say()
    {
        // 畫布.顯示
        objCanvas.SetActive(true);
        if (countPlayer >= countFinish) _state = state.complete;


        // 判斷式(狀態)
        switch (_state)
        {
            case state.start:
                StartCoroutine(ShowDialog());            // 開始對話
                _state = state.notComplete;
                break;
            case state.notComplete:
                textSay.text = sayNotComplete;      // 未完成對話
                break;
            case state.complete:
                textSay.text = sayComplete;         // 完成對話
                break;
        }
    }

    private IEnumerator ShowDialog()
    {
        textSay.text = "";                              // 清空文字

        for (int i = 0; i < sayStart.Length; i++)       // 迴圈跑對話.長度
        {
            textSay.text += sayStart[i].ToString();
            aud.PlayOneShot(soundSay, 0.6f);
            yield return new WaitForSeconds(speed);     // 等待
        }
    }


    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        objCanvas.SetActive(false);
    }

    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        countPlayer++;
    }
}
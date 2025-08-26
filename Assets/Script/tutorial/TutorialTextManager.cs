using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextManager : MonoBehaviour
{
    [SerializeField] List<Text> text = new List<Text>();

    [SerializeField] private TutorialText TextData;

    int nowCount = 0;
    int textCount = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (text.Count != 0)
        {

            for (int i = 0; i < text.Count -1; i++)
            {
                text[0].text = TextData.TextListData[nowCount].TextList[0];

            }

            text[1].text = TextData.TextListData[nowCount].TextList[textCount];

            if (nowCount == 3)
            {
                text[2].gameObject.SetActive(true);

                text[2].text = TextData.TextListData[nowCount].TextList[2];
            }
            else
            {
                text[2].gameObject.SetActive(false);
            }
        }
    }

    public void Count(int _num)
    {
        nowCount += _num;
    }

    public int GetNowCount()
    {
        return nowCount;
    }

    public int GetTextCount()
    {
        return textCount;
    }

    /// <summary>
    /// 1 or 2
    /// </summary>
    /// <param name="_num"></param>
    public void TextCount(int _num)
    {
        textCount = _num;
    }
}

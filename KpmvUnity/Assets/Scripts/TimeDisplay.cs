using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Update()
    {
        // 현재 시간 가져오기
        DateTime currentTime = DateTime.Now;

        // 시간 텍스트 업데이트
        timeText.text = currentTime.ToString("HH:mm:ss");
    }
}

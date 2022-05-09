using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBlue : MonoBehaviour
{
    // 이동할 지점들을 담을 배열 선언
    public static Transform[] bluePoints;

    void Awake()
    {
        // 배열의 크기 선언
        // child.Count: 본 스크립트를 포함하고 있는 오브젝트의 자식 개수
        // 자식 오브젝트를 찾는 방법들
        // transform.FindChild("이름"); transform.GetChild(번호);
        bluePoints = new Transform[transform.childCount];

        // 배열의 인덱스마다 자식의 Transform 저장
        for (int i = 0; i < bluePoints.Length; i++)
        {
            bluePoints[i] = transform.GetChild(i);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI nftText;

    public static List<MainClient.ObjP> pdbList;

    public int randomIndex;

    bool textOn = false;

    void Update()
    {
        if (!textOn)
        {
            pdbList = MainClient.pdbList;
            randomIndex = Random.Range(0, pdbList.Count);
            nftText.text = "¿Ã∏ß : " + pdbList[randomIndex].mName + " / NFT : " + pdbList[randomIndex].mNftAddr;
            textOn = true;
        }

    }
}

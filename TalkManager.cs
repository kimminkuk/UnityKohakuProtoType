using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TalkManager : MonoBehaviour
{

    Dictionary<int, string[]> talkData;

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        Generate();
        GenerateGetTextFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate() {

        // 1. 다이렉트로 넣는 방식
        talkData.Add((int)eNpc.NPC_PROG, 
            new string[] {"[PROG] MBTI 테스트1:0", "[PROG] MBTI 테스트2:1"});
        talkData.Add((int)eNpc.NPC_DUDE, 
            new string[] {"[DUDE] MBTI 테스트1:0", "[DUDE] MBTI 테스트2:1"});
        talkData.Add((int)eNpc.NPC_PINKMAN, 
            new string[] {"[PINKMAN] MBTI 테스트1:0", "[PINKMAN] MBTI 테스트2:1"});
        talkData.Add((int)eNpc.NPC_ANONYMOUS,
            new string[] {"[ANONYMOUS] MBTI 테스트1:0", "[ANONYMOUS] MBTI 테스트2:1"});
    }

    void GenerateGetTextFile() {

        string filePath = Application.dataPath + "/Codes/TextFile/MBTI_Text.txt";
        string textContent = "";
        if (File.Exists(filePath))
        {
            textContent = File.ReadAllText(filePath);
            Debug.Log("File content: " + "\n" + textContent);
        }
        else
        {
            Debug.Log("File not Found: " + filePath);
        }

        // 나중에 문자열 처리로 넣을 수 있다.

    }

    void GenerateGetDB() {

    }
    
    public string GetTalk(int objId, int talkIndex) {
        
        if (talkIndex >= talkData[objId].Length) {
            return null;
        } else {
            return talkData[objId][talkIndex];
        }
    }
}

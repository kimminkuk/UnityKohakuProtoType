using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TalkManager : MonoBehaviour
{

    Dictionary<int, string[]> talkData;
    Dictionary<int, List<string>> talkDataVer2;

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        talkDataVer2 = new Dictionary<int, List<string>>();
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
        string filePathNpcDude = Application.dataPath + "/Codes/TextFile/MBTI_DUDE.txt";
        string filePathNpcProg = Application.dataPath + "/Codes/TextFile/MBTI_PROG.txt";
        string filePathNpcPinkman = Application.dataPath + "/Codes/TextFile/MBTI_PINKMAN.txt";
        string textContent = "";
        
        string[] filePathArr = new string[] {filePath, filePathNpcDude, filePathNpcProg, filePathNpcPinkman};
        int[] fileNpcId = new int[] {-1000, (int)eNpc.NPC_DUDE, (int)eNpc.NPC_PROG, (int)eNpc.NPC_PINKMAN};
        string[] fileNpcName = new string[] {"EMPTY", "DUDE", "PROG", "PINKMAN"};

        for (int i = 0; i < filePathArr.Length; i++) {
            List<string> fileContentList = new List<string>();
            using (StreamReader reader = new StreamReader(filePathArr[i]))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    fileContentList.Add(line.Split("<<<")[0]);
                }
            }
            if (fileContentList.Count > 0) {
                talkDataVer2.Add( fileNpcId[i], fileContentList);
            }            
        }

#if false
        if (File.Exists(filePath))
        {
            textContent = File.ReadAllText(filePath);

            //step 1) textContent의 내용을 가져옵니다. 
            textContent = File.ReadAllText(filePath);
            //step 1-1) find를 이용해서 [NPC.NAME]을 찾는다.
            //step 2) [NPC.NAME]으로 구분한다

            //step 1-2) 파일을 NPC.NAME으로 구분한다.
            textContentNpcDude = File.ReadAllText(filePathNpcDude);
            textContentNpcProg = File.ReadAllText(filePathNpcProg);
            textContentNpcPinkman = File.ReadAllText(filePathNpcPinkman);

            
            //step 3) step2에서 구분한 내용들을 :로 구분한다

            //step 4) npcId에 맞게 넣는다.
            talkDataVer2.Add(
                (int)eNpc.NPC_DUDE, 
                textContentNpcDude.Split(':')
            );

            Debug.Log("File content: " + "\n" + textContent);
        }

        else
        {
            Debug.Log("File not Found: " + filePath);
        }
#endif
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

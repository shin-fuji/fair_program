using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadBehavFileToggleScript : MonoBehaviour
{
    Toggle readBehavFileToggle;

    void Start()
    {
        readBehavFileToggle = GetComponent<Toggle>();

        // スタート時は Toggle を BehaviourScriptReader.readFileOrNot に合わせる
        readBehavFileToggle.isOn = BehaviourScriptReader.readFileOrNot;
    }

    void Update()
    {
        //Debug.Log(readBehavFileToggle.isOn);
    }

    public void ChangeReadBehavFileToggle()
    {
        //Debug.Log("Toggleが変更されました");

        BehaviourScriptReader.readFileOrNot = readBehavFileToggle.isOn;
    }
}

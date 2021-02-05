using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public NetManager netmanager;
    public InputField input;             // 注意这儿不能直接设置Text并将input field中的Text拖入，会造成text无法修改，傻逼unity
    void Start()
    {
    }
    public void MySendMessage()
    {
        if (input.text != "")
        {
            netmanager.MySendMessage(input.text);
            input.text = "";
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

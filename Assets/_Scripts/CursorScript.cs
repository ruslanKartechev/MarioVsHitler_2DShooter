using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CursorScript : MonoBehaviour
{

    public Texture2D ingame;
    public Texture2D inMenu;
    // Start is called before the first frame update
    private static CursorScript S;
    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    public static void SetMenuCursor()
    {
        Cursor.SetCursor(S.inMenu,Vector2.zero, CursorMode.Auto);
    }
    public static void SetGameCursor()
    {
        Cursor.SetCursor(S.ingame, Vector2.zero, CursorMode.Auto);
    }

    public static void HideCursor()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

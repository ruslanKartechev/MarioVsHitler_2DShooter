using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CursorScript: MonoBehaviour
{

    public Texture2D inGame;
    public Texture2D inMenu;

    public  void SetMenuCursor()
    {
        Cursor.SetCursor(inMenu,Vector2.zero, CursorMode.Auto);
    }
    public  void SetGameCursor()
    {
        Cursor.SetCursor(inGame, Vector2.zero, CursorMode.Auto);
    }

    public  void HideCursor()
    {

    }
    
}

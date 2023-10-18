using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerImage
{
    public static Sprite GetSprite(int spriteChoice)
    {
        switch (spriteChoice)
        {
            case 0: return Resources.Load<Sprite>("_player1");
            case 1: return Resources.Load<Sprite>("_player2");
        }

        return null;
    }
}

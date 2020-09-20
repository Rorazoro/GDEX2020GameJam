using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicHelperUI : MonoBehaviour
{
    public DiscreteMagicController Controller;
    public Text Text;

    void Update()
    {
        if(!Controller.IsCasting && !Controller.IsSpellActive)
        {
            Text.text = "Right click to start casting";
        }else if (Controller.IsCasting)
        {
            Text.text = "Left Click to draw a pattern \n Right click to cancel";
        }
        else if (Controller.IsSpellActive)
        {
            Text.text = "Casting Spell: "+Controller.SpellId+"\nRight click to cancel";
        }
    }
}

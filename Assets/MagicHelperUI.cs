using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MagicHelperUI : MonoBehaviour
{
    public DiscreteMagicController Controller;
    public Text Text;
    private Image Image;

    private void Start()
    {
        Image = GetComponent<Image>();
    }

    void Update()
    {
        if(Controller.SpellCasters.Length == 0)
        {
            Text.text = "Look around for a magical object.";
        }
        else if (!Controller.IsCasting && !Controller.IsSpellActive )
        {

            Text.text = "Click left mouse button to cast a spell.\n";
            List<string> spellIds = new List<string>();
            foreach (var spellCaster in Controller.SpellCasters)
            {
                spellIds.Add(spellCaster.GetSpellId());
            }

            Text.text += $"Spells: {string.Join(",", spellIds)}";

        }
        else if (Controller.IsCasting)
        {
            Text.text = "Hold down left mouse button to draw a pattern";
        }
        else if (Controller.IsSpellActive)
        {
            Text.text = $"Casting spell \"{Controller.SpellId}\"\nClick left mouse button to finish";
        }
    }
}

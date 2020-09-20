using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICastSpell
{
    /// in your implementation, call magicController.EndSpell() to finish the spell
    /// in yout implementation, listen to magicController.OnEndCast() to handle the spell cancellation
    void CastSpell(DiscreteMagicController magicController);

    string GetSpellId();

    bool DoLockMouse();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICastable : IInteractable {
    /// in your implementation, call magicController.EndSpell() to finish the spell
    /// in yout implementation, listen to magicController.OnEndCast() to handle the spell cancellation
    void CastSpell ();
    void EndSpell ();
    string GetSpellId ();

    //bool DoLockMouse ();
}
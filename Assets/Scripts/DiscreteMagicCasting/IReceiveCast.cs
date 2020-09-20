using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiveCast
{
    /// <summary>
    /// in your implementation, call magicController.EndSpell() to finish the spell
    /// in yout implementation, listen to magicController.OnEndCast() to handle the spell cancellation
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="spellId"></param>
    void CastSpell(DiscreteMagicController magicController, string spellId);
}

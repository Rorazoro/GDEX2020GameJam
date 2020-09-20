using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiveCast
{
    /// <summary>
    /// in your implementation, make sure caster.EndCast() is called if the spell is stopped 
    /// in yout implementation, make sure to listen to OnEndCast() to approapriateley handle the spell cancellation
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="spellId"></param>
    void CastSpell(Caster caster, string spellId);
}

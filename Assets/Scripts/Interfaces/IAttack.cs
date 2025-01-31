using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IAttack
    {
        void TryExecuteAttack(IAttackable target, float distance);
    }
}
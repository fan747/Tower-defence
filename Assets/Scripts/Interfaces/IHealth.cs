using System;

namespace Assets.Scripts.Interfaces
{
    public interface IHealth
    {
        event Action DieAction;
        void TakeHealth(float health = 1);
    }
}

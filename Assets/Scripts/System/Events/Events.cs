
using UnityEngine;

namespace Events
{
    public class GameEvents
    {
        public System.Action OnGameLoaded { get; set; }
        public System.Action OnGameStart { get; set; }
        public System.Action OnNextGame { get; set; }
        public System.Action OnRestartGame { get; set; }
        public System.Action OnToMainMenu { get; set; }
        public System.Action<GameResult> OnGameFinish { get; set; }

        public enum GameResult
        {
            Victory = 0,
            Death = 1,
        }
    }

    public class UnitEvents
    {
        public System.Action<IUnitDeathContext> OnUnitDeath {get; set; }
        public System.Action<IUnit> OnUnitCreated { get; set; }
        public System.Action<string, string> OnUnitWeaponChange { get; set; }
    }

    public class ItemEvents
    {
        public System.Action<IGameItem> OnItemPlayerCollision { get; set; }
    }
}
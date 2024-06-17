using System;

namespace Events
{
    public static class GameEventsContainer
    {
        public static Action OnTableCardPlaced { get; set; }
        public static Action OnAllTableCardsBit { get; set; }
        public static Action OnPlayerTurnEnd { get; set; }
        public static Action OnPlayerPressTake { get; set; }
        public static Action ShouldClearTable { get; set; }
        public static Action OnPlayerPressPass { get; set; }
        public static Action OnPlayerPressedPassAfterTake { get; set; }
        public static Action ShouldDisplayPassAfterTake { get; set; }
        public static Action OnNextPlayerAssigned { get; set; }
        public static Action OnMoveEndAfterWin { get; set; }
    }
}
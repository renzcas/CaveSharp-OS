using System;

namespace CaveSharpOS.Time
{
    /// <summary>
    /// Handles timed world events, narrative beats, CTA triggers,
    /// and realm-wide synchronization pulses. This engine is the
    /// heartbeat of CaveSharp-OS's event-driven architecture.
    /// </summary>
    public class EventEngine
    {
        public float GlobalTime { get; private set; } = 0f;
        public float NextEventAt { get; private set; } = 5f;

        /// <summary>
        /// Update global time and fire events when thresholds are reached.
        /// </summary>
        public void Update(float dt)
        {
            GlobalTime += dt;

            if (GlobalTime >= NextEventAt)
            {
                TriggerEvent();
                NextEventAt += 5f; // schedule next event
            }

            // Example debug:
            // Console.WriteLine($"[EventEngine] Time={GlobalTime:0.00} Next={NextEventAt:0.00}");
        }

        /// <summary>
        /// Fires a timed event.
        /// </summary>
        private void TriggerEvent()
        {
            // Placeholder event logic
            // Console.WriteLine("[EventEngine] World event triggered!");
        }
    }
}

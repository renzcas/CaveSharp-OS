using System;
using System.Collections.Generic;
using System.Linq;

namespace CaveSharp.MetaAI
{
    /// <summary>
    /// CyberDefenseAgent monitors logs, detects anomalies,
    /// and contributes to the global threat level.
    /// Overseer.cs uses this to compute threat and broadcast telemetry.
    /// </summary>
    public class CyberDefenseAgent
    {
        public float ThreatLevel { get; private set; } = 0f;

        private readonly List<float> _history = new();
        private readonly int _window = 50;

        public void AnalyzeLog(string log)
        {
            float score = ScoreLog(log);

            ThreatLevel += score;
            if (ThreatLevel > 100) ThreatLevel = 100;

            AddHistory(score);

            if (IsAnomaly(score))
            {
                Console.WriteLine($"[CyberDefense] Anomaly detected: {log}");
            }
        }

        /// <summary>
        /// Simple scoring system for logs.
        /// Replace with your own logic later.
        /// </summary>
        private float ScoreLog(string log)
        {
            log = log.ToLower();

            if (log.Contains("unauthorized")) return 5f;
            if (log.Contains("failed login")) return 3f;
            if (log.Contains("breach")) return 10f;
            if (log.Contains("malware")) return 8f;

            return 1f; // normal noise
        }

        private void AddHistory(float value)
        {
            _history.Add(value);
            if (_history.Count > _window)
                _history.RemoveAt(0);
        }

        private bool IsAnomaly(float value)
        {
            if (_history.Count < 5) return false;

            float mean = _history.Average();
            float variance = _history.Select(v => (v - mean) * (v - mean)).Average();
            float std = (float)Math.Sqrt(variance);

            return Math.Abs(value - mean) > std * 2.5f;
        }

        /// <summary>
        /// Natural decay of threat over time.
        /// Overseer calls this every tick.
        /// </summary>
        public void Decay(float dt)
        {
            ThreatLevel -= dt * 0.2f;
            if (ThreatLevel < 0) ThreatLevel = 0;
        }
    }
}

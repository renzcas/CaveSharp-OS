using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace CaveSharpOS.Systems
{
    public sealed class StateLogger
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            IncludeFields = true
        };
        private readonly object _writeLock = new();

        public StateLogger(string filePath = "logs/world_state.jsonl")
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

            _filePath = Path.GetFullPath(filePath);
            var directory = Path.GetDirectoryName(_filePath);
            if (directory is not null)
                Directory.CreateDirectory(directory);
        }

        public void Log<TSnapshot>(long tick, TSnapshot snapshot, string source)
        {
            ArgumentNullException.ThrowIfNull(snapshot);
            ArgumentException.ThrowIfNullOrWhiteSpace(source);

            var entry = new LogEntry<TSnapshot>(tick, DateTimeOffset.UtcNow, source, snapshot);
            var jsonLine = JsonSerializer.Serialize(entry, _jsonOptions) + Environment.NewLine;

            lock (_writeLock)
            {
                File.AppendAllText(_filePath, jsonLine, new UTF8Encoding(false));
            }
        }

        public void Log(long tick, Dictionary<string, object> snapshot) =>
            Log(tick, snapshot, "TimeEngine");

        private sealed record LogEntry<TSnapshot>(
            long Tick,
            DateTimeOffset Timestamp,
            string Source,
            TSnapshot Snapshot);
    }
}

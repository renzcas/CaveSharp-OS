using System;

namespace RB_App
{
    public class Dashboard
    {
        private readonly CTAPanel _ctaPanel;
        private readonly CreaturePanel _creaturePanel;
        private readonly CorruptionHeatmapPanel _heatmapPanel;

        public Dashboard()
        {
            _ctaPanel = new CTAPanel();
            _creaturePanel = new CreaturePanel();
            _heatmapPanel = new CorruptionHeatmapPanel();
        }

        public void UpdateCTA(string status)
        {
            _ctaPanel.Update(status);
        }

        public void UpdateCreatureCount(int count)
        {
            _creaturePanel.Update(count);
        }

        public void UpdateHeatmap(int[,] map)
        {
            _heatmapPanel.Update(map);
        }

        public void Render()
        {
            Console.WriteLine("=== RB-App Cockpit ===");
            Console.WriteLine();

            _ctaPanel.Render();
            Console.WriteLine();

            _creaturePanel.Render();
            Console.WriteLine();

            _heatmapPanel.Render();
            Console.WriteLine();
        }
    }
}

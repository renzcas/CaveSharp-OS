using System;

namespace RB_App
{
    public class CTAPanel
    {
        private string _status = "Idle";

        public void Update(string status)
        {
            _status = status;
        }

        public void Render()
        {
            Console.WriteLine("=== CTA Panel ===");
            Console.WriteLine($"Status: {_status}");
        }
    }
}

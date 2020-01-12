using System.Collections.Generic;

namespace ClaimsReserving.Models
{
    public class Defects
    {
        public Defects()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        public string FileName { get; set; }

        public List<string> Errors { get; set; }

        public List<string> Warnings { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraNemonicIntegration.Models
{
    public class Issues
    {
        public List<string> keys { get; set; }

        public List<string> labels { get; set; }

        public List<string> summaries { get; set; }

        public List<string> storypoints { get; set; }

        public string message { get; set; }

        public Issues()
        {
            keys = new List<string>();
            labels = new List<string>();
            summaries= new List<string>();
            storypoints = new List<string>();
            message = "";
        }
    }
}
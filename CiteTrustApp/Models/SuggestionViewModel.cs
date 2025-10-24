using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CiteTrustApp.Models
{
    public class SuggestionViewModel
    {
        public List<string> Keywords { get; set; } = new List<string>();
        public List<string> Suggestions { get; set; } = new List<string>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CiteTrustApp.Models
{
    public class EvidenceAnalysisViewModel
    {   public int EvidenceId { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string AnalysisText { get; set; }
        public List<string> KeyPoints { get; set; }
        public string SuggestedCitation { get; set; }
    }
}
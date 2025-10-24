using CiteTrustApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CiteTrustApp.Services
{
    public class AISuggestionService
    {
        private readonly CTSDbContext _db;

        public AISuggestionService(CTSDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // Build simple suggestions based on recent user keywords or global top keywords.
        public SuggestionViewModel GetSuggestions(int? userId = null, int? categoryId = null, int? sourceId = null, int max = 5)
        {
            // Gather candidate keywords
            var keywords = new List<string>();

            if (userId.HasValue)
            {
                keywords = _db.SearchLogs
                    .Where(s => s.UserId == userId.Value && s.Keyword != null)
                    .OrderByDescending(s => s.SearchTime)
                    .Select(s => s.Keyword)
                    .Distinct()
                    .Take(10)
                    .ToList();
            }

            if (!keywords.Any())
            {
                // fallback to global top keywords
                keywords = _db.SearchLogs
                    .Where(s => s.Keyword != null)
                    .GroupBy(s => s.Keyword)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .Take(10)
                    .ToList();
            }

            // Build suggestions by finding recent evidences that match keywords or filters
            var suggestions = new List<string>();
            var query = _db.Evidences.Include("Source").AsQueryable();

            if (categoryId.HasValue) query = query.Where(e => e.CategoryId == categoryId.Value);
            if (sourceId.HasValue) query = query.Where(e => e.SourceId == sourceId.Value);

            // Try keyword-based matches first
            foreach (var kw in keywords)
            {
                if (string.IsNullOrWhiteSpace(kw)) continue;
                var matches = query
                    .Where(e => (e.Content != null && e.Content.Contains(kw)) ||
                                (e.Source != null && e.Source.Title != null && e.Source.Title.Contains(kw)))
                    .OrderByDescending(e => e.CreatedAt)
                    .Take(3)
                    .ToList();

                foreach (var m in matches)
                {
                    var title = m.Source?.Title ?? (m.Content ?? "").Trim();
                    var snippet = (m.Content ?? (m.Source?.Title ?? "")).Trim();
                    if (snippet.Length > 140) snippet = snippet.Substring(0, 140).Trim() + "...";
                    var text = $"\"{kw}\": {title} — {snippet}";
                    if (!suggestions.Contains(text)) suggestions.Add(text);
                    if (suggestions.Count >= max) break;
                }
                if (suggestions.Count >= max) break;
            }

            // If still empty, show recent evidences
            if (!suggestions.Any())
            {
                var recent = query.OrderByDescending(e => e.CreatedAt).Take(max).ToList();
                foreach (var r in recent)
                {
                    var title = r.Source?.Title ?? (r.Content ?? "").Trim();
                    var snippet = (r.Content ?? (r.Source?.Title ?? "")).Trim();
                    if (snippet.Length > 140) snippet = snippet.Substring(0, 140).Trim() + "...";
                    var text = $"{title} — {snippet}";
                    if (!suggestions.Contains(text)) suggestions.Add(text);
                }
            }

            return new SuggestionViewModel
            {
                Keywords = keywords.Distinct().Take(10).ToList(),
                Suggestions = suggestions.Take(max).ToList()
            };
        }
    }
}
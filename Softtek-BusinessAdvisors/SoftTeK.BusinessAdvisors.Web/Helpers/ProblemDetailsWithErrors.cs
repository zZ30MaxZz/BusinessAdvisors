﻿namespace SoftTeK.BusinessAdvisors.Web.Helpers
{
    public class ProblemDetailsWithErrors
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}

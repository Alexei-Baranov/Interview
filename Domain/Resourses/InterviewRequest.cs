using System;
using System.Collections.Generic;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Enums;

namespace InterviewService.Domain.Resourses
{
    public class InterviewRequest
    {
        public string Question { get; set; }
        
        public bool AllOptionRequired { get; set; }
        public int MaxSelectedOptionCount { get; set; }
        public DateTime InterviewStart { get; set; }
        public DateTime? InterviewEnd { get; set; }
        
        public int ResponsesCountForPublish { get; set; }
        public bool ResponsesIsPublic { get; set; }
        
        public bool IsPublished { get; set; }
    }
}
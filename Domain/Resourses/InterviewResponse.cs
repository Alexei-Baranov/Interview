using System;
using System.Collections.Generic;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Enums;

namespace InterviewService.Domain.Resourses
{
    public class InterviewResponse
    {
        public int Id { get; set; }
        public DateTimeOffset CreationTimestamp { get; set; }
        
        public string Question { get; set; }
        
        public bool AllOptionRequired { get; set; }
        public int MaxSelectedOptionCount { get; set; }
        
        public IList<OptionResponse> Options { get; set; }
        
        public DateTime InterviewStart { get; set; }
        public DateTime? InterviewEnd { get; set; }
        
        public int ResponsesCountForPublish { get; set; }
        public bool ResponsesIsPublic { get; set; }
        
        public bool IsPublished { get; set; }
        
        public string CreatedUserId { get; set; }
    }
}
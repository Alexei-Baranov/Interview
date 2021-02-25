using System;
using System.Collections.Generic;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Enums;

namespace InterviewService.Domain.Resourses
{
    public class InterviewRequest
    {
        
        public int Id { get; set; }
        
        public string Question { get; set; }
        
        public bool AllOptionRequired { get; set; }
        public bool InterviewUnlimited { get; set; }
        public DateTime? InterviewEnd { get; set; }
        
        public int ResponsesCountForPublish { get; set; }
        public bool ResponsesIsPublic { get; set; }
        
        public InterviewStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
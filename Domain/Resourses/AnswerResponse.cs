using System;
using InterviewService.Domain.Entities;

namespace InterviewService.Domain.Resourses
{
    public class AnswerResponse
    {
        public int Id { get; set; }
        
        public DateTimeOffset CreationTimestamp { get; set; }
        
        public int InterviewId { get; set; }
        
        public int OptionId { get; set; }
        
        public string UserId { get; set; }
    }
}
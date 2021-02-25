using System;
using System.Collections.Generic;

namespace InterviewService.Domain.Entities
{
    public class Answer
    {
        
        public static Answer Create()
        {
            return new Answer()
            {
                CreationTimestamp = DateTimeOffset.Now
            };
        }
        
        public DateTimeOffset CreationTimestamp { get; set; }
        
        public int Id { get; set; }
        
        public Interview Interview { get; set; } 
        public int InterviewId { get; set; }
        
        public Option Option { get; set; }
        public int OptionId { get; set; }
        
        public string UserId { get; set; }
    }
}
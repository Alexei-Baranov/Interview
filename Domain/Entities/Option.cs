using System;
using System.Collections.Generic;

namespace InterviewService.Domain.Entities
{
    public class Option
    {
        
        public static Option Create()
        {
            return new Option()
            {
                CreationTimestamp = DateTimeOffset.Now
            };
        }
        
        public DateTimeOffset CreationTimestamp { get; set; }
        
        public int Id { get; set; }
        
        public Interview Interview { get; set; }
        
        public int InterviewId { get; set; }
        
        public string Title { get; set; }

        public List<Answer> Answers { get; set; } = new();

    }
}
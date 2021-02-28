using System;
using System.Collections.Generic;

namespace InterviewService.Domain.Entities
{
    public class Option
    {
        public Option()
        {
            CreationTimestamp = DateTimeOffset.Now;
            Answers = new List<Answer>();
        }
        
        public DateTimeOffset CreationTimestamp { get; set; }
        
        public int Id { get; set; }
        
        public Interview Interview { get; set; }
        
        public int InterviewId { get; set; }
        
        public string Title { get; set; }

        public IList<Answer> Answers { get; set; }
        
        public int AnswerCount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using InterviewService.Domain.Enums;

namespace InterviewService.Domain.Entities
{
    public class Interview
    {
        public Interview()
        {
            Answers = new List<Answer>();
            Options = new List<Option>();
            CreationTimestamp = DateTimeOffset.Now;
        }

        public int Id { get; set; }
        public DateTimeOffset CreationTimestamp { get; set; }
        
        public string Question { get; set; }
        
        public bool AllOptionRequired { get; set; }
        public int MaxSelectedOptionCount { get; set; }
        public IList<Option> Options { get; set; }
        
        public IList<Answer> Answers { get; set; }
        
        public DateTime InterviewStart { get; set; }
        public DateTime? InterviewEnd { get; set; }
        
        public int ResponsesCountForPublish { get; set; }
        public bool ResponsesIsPublic { get; set; }
        
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }

        public string CreatedUserId { get; set; }

        public const int MaxOptionCount = 10;
        
        public void AddAnswer(Answer answer)
        {
            Answers.Add(answer);
        }

        public void AddOption(Option option)
        {
            Options.Add(option);
        }

        public void RemoveOption(Option option)
        {
            Options.Remove(option);
        }
        
    }
}
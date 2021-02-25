using System;
using System.Collections.Generic;
using InterviewService.Domain.Enums;

namespace InterviewService.Domain.Entities
{
    public class Interview
    {
        public static Interview Create()
        {
            return new Interview()
            {
                CreationTimestamp = DateTimeOffset.Now
            };
        }
        
        
        public Interview()
        {
        }

        public int Id { get; set; }
        public DateTimeOffset CreationTimestamp { get; set; }
        
        public string Question { get; set; }
        
        public bool AllOptionRequired { get; set; }
        public int SelectedOptionCount { get; set; }
        public IList<Option> Options { get; } = new List<Option>();
        
        public IList<Answer> Answers { get; } = new List<Answer>();
        
        public bool InterviewUnlimited { get; set; }
        public DateTime? InterviewEnd { get; set; }
        
        public int ResponsesCountForPublish { get; set; }
        public bool ResponsesIsPublic { get; set; }
        
        public InterviewStatus Status { get; set; }
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
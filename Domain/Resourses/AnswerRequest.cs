using System;
using System.Collections.Generic;
using InterviewService.Domain.Entities;

namespace InterviewService.Domain.Resourses
{
    public class AnswerRequest
    {
        public List<int> OptionsId { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ParticipantDetail
    {
        public int ParticipantDetailID { get; set; }
        public Nullable<int> ParticipantID { get; set; }
        public Nullable<int> QuizID { get; set; }
        public Nullable<int> SelectedOption { get; set; }
        public Nullable<System.DateTime> SubmissionTime { get; set; }
        public Nullable<int> TimeSpent { get; set; }
    }
}

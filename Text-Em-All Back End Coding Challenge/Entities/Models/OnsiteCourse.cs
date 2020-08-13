using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class OnsiteCourse
    {
        public int CourseId { get; set; }
        public string Location { get; set; }
        public string Days { get; set; }
        public TimeSpan Time { get; set; }

        public virtual Course Course { get; set; }
    }
}

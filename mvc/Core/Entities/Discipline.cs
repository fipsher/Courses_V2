﻿using System.Collections.Generic;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class Discipline : Entity
    {
        public string Name { get; set; }
        public string ProviderCathedraId { get; set; }
        public List<string> SubscriberCathedraIds { get; set; }
        public DisciplineType DisciplineType { get; set; }
        public int Semester { get; set; }
        public string Description { get; set; }
        public string LecturerId { get; set; }

        public List<string> StudentIds { get; set; }
    }
}

﻿using Core.Entities;
using Core.Helpers;
using Core.Responces;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IDisciplineService : IService<Discipline>
    {
        List<DisciplineResponce> FindDisciplineResponse(SearchFilter<Discipline> filter, bool includingSubscriberCathedras = false, bool includingStudents = false);
    }
}

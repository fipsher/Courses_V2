﻿//using Core.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace LNU.Courses.BLL.DeadlinesBLL
//{
//    public class SecondDeadline
//    {
//        private readonly IServiceFactory _serviceFactory;
//        private const int MaxCourse = 5;

//        public SecondDeadline(IServiceFactory serviceFactory)
//        {
//            _serviceFactory = serviceFactory;
//        }
//        private void manUpGroupsFirstly(int course, int semestr)
//        {
//            var students = ctx.Students.Where(std => std.course == course);
//            var groups = ctx.Group.Where(g => !g.Deleted && g.Disciplines.course == semestr)
//                                    .Where(g => g.Status && g.Wave == 1 && g.AmountOfStudent > 25 || g.Wave == 2).ToList();


//            ////comm?
//            //var groupsFrstWave = _repository.GetGroups().Where(gr =>
//            //{
//            //    var singleOrDefault = disciplines.SingleOrDefault(d => d.id == gr.disciplinesID);///
//            //    return singleOrDefault != null && singleOrDefault.course == semestr && gr.Deleted == false
//            //});
//            //var stdInGroups = ctx.StudentsInGroups.Where(sig => groups.Any(gr => gr.id == sig.groupID)).ToList();
//            var stdInGroups = ctx.StudentsInGroups.Where(sig => sig.Group != null).ToList();


//            //find std id that not registered
//            //var notRegisteredStd = (from std in students
//            //                        join sig in stdInGroups on std.id equals sig.studentID into loj
//            //                        from l in loj.DefaultIfEmpty()
//            //                        where l == null
//            //                        select std).ToList();
//            var notRegisteredStd = students.Where(std => std.StudentsInGroups.Count(sig => sig.Group.Disciplines.course == semestr) == 0).ToList();


//            //get IQueriable that have groupId and std count
//            var smallSig = (from gr in groups
//                            orderby gr.AmountOfStudent descending
//                            where gr.Wave == 2 && gr.Deleted == false && gr.Disciplines.Group.Any(g => !g.Deleted && g.AmountOfStudent >= 25 && g.Wave == 1)
//                            select new { GroupId = gr.id, Count = gr.AmountOfStudent }).ToList();


//            var groupIndexer = 0;

//            if (smallSig.Count != 0)
//            {
//                for (int i = 0; i < notRegisteredStd.Count; i++)
//                {
//                    if (groupIndexer >= smallSig.Count)
//                    {
//                        groupIndexer = 0;
//                    }
//                    _repository.AddStudentInGroups(new StudentsInGroups()
//                    {
//                        studentID = notRegisteredStd[i].id,
//                        groupID = smallSig[groupIndexer].GroupId,
//                        DateOfRegister = DateTime.Now
//                    });

//                    groupIndexer++;
//                }
//            }
//        }

//        private void manUpGroupsSecondly(int course, int semestr)
//        {
//            var students = _repository.GetStudents().Where(std => std.course == course);
//            var disciplines = _repository.GetDisciplines();

//            var groups = _repository.GetGroups().Where(gr =>
//            {
//                var singleOrDefault = disciplines.SingleOrDefault(d => d.id == gr.disciplinesID);///
//                return singleOrDefault != null && singleOrDefault.course == semestr && gr.Deleted == false && gr.Wave == 2;
//            });
//            var groupsFrstWave = _repository.GetGroups().Where(gr =>
//            {
//                var singleOrDefault = disciplines.SingleOrDefault(d => d.id == gr.disciplinesID);///
//                return singleOrDefault != null && singleOrDefault.course == semestr && gr.Deleted == false && gr.Wave == 1;
//            });
//            var stdInGroups = _repository.GetStudentsInGroups().Where(sig => groupsFrstWave.Any(gr => gr.id == sig.groupID) || groups.Any(gr => gr.id == sig.groupID));

//            var studentsInGroups = stdInGroups as IList<StudentsInGroups> ?? stdInGroups.ToList();
//            var studentsList = students as IList<Students> ?? students.ToList();

//            //find std id that registered only once
//            var temp = from std in studentsList
//                       join sig in studentsInGroups on std.id equals sig.studentID into loj
//                       from l in loj.DefaultIfEmpty()
//                       where l != null
//                       group std by std.id into grp
//                       where grp.Count() == 1
//                       select grp.Key;
//            //get students by theirs id 
//            var onceRegisteredStd = (from std in studentsList
//                                     join t in temp on std.id equals t
//                                     select std).ToList();

//            // get IQueriable that have groupId and std count          
//            var grList = groups as IList<Group> ?? groups.ToList();
//            var smallSig = (from gr in grList
//                            orderby gr.AmountOfStudent descending
//                            where gr.Wave == 2 && gr.Deleted == false
//                            select new { GroupId = gr.id, Count = gr.AmountOfStudent, DiscId = gr.disciplinesID }).ToList();

//            var groupIndexer = 0;

//            if (smallSig.Count != 0)
//            {
//                int i = 0;
//                while (onceRegisteredStd.Count != 0)
//                {
//                    if (groupIndexer >= smallSig.Count)
//                    {
//                        groupIndexer = 0;
//                    }
//                    var frstWaveGroup = groupsFrstWave.First(el => el.disciplinesID == smallSig[groupIndexer].DiscId);
//                    if (studentsInGroups.Where(el => el.groupID == smallSig[groupIndexer].GroupId)
//                                        .All(el => el.studentID != onceRegisteredStd[i].id) && 
//                        studentsInGroups.Where(el => el.groupID == frstWaveGroup.id)
//                                        .All(el => el.studentID != onceRegisteredStd[i].id))
//                    {
//                        _repository.AddStudentInGroups(new StudentsInGroups()
//                        {
//                            studentID = onceRegisteredStd[i].id,
//                            groupID = smallSig[groupIndexer].GroupId,
//                            DateOfRegister = DateTime.Now
//                        });
//                        onceRegisteredStd.Remove(onceRegisteredStd[i]);
//                        i = 0;
//                        groupIndexer++;
//                    }
//                    else
//                    {
//                        if (i < onceRegisteredStd.Count - 1)
//                        {
//                            i++;
//                        }
//                        else
//                        {
//                            groupIndexer++;
//                        }
//                    }
//                }
//            }
//        }

//        public void FillGroupsWithRemainedStd(int semestr)
//        {
//            for (int i = 1; i < MaxCourse; i++)
//            {
//                manUpGroupsFirstly(i, i * 2 + 1);
//                manUpGroupsFirstly(i, i * 2 + 2);
//            }

//            //for (int i = 1; i < MaxCourse; i++)
//            //{
//            //    manUpGroupsSecondly(i, i * 2 - 1);
//            //    manUpGroupsSecondly(i, i * 2);
//            //}
//        }

//    }
//}

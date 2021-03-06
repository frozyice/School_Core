﻿using School_Core.Querys;
using School_Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace School_Core.ViewModels.Lecture
{
    public class LectureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public LectureStatus Status { get; set; }

        public interface IProvider
        {
            IEnumerable<LectureViewModel> Provide();
        }
        public class Provider : IProvider
        {
            private readonly ILectureQuery _query;

            public Provider(ILectureQuery query)
            {
                _query = query;
            }
            public IEnumerable<LectureViewModel> Provide()
            {

                var lectures = _query.GetLectures();
                var lectureViewModels = new List<LectureViewModel>();
                foreach (var lecture in lectures)
                {
                    lectureViewModels.Add(new LectureViewModel()
                    {
                        Id = lecture.Id,
                        Name = lecture.Name ?? "",
                        Status = lecture.Status
                    });
                }
                return lectureViewModels;
            }
        }

    }
}

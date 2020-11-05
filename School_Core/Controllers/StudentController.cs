using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School_Core.API.DTOs;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;
using School_Core.ViewModels.Students;

namespace School_Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly IQuery<Student> _query;
        private readonly IDefaultHttpClient _httpClient;
        private readonly StudentListViewModel.IProvider _studentListViewmodelProvider;
        private readonly StudentMedicalViewModel.IProvider _medicalViewModelProvider;

        public StudentController(IQuery<Student> query, IDefaultHttpClient httpClient, StudentListViewModel.IProvider studentListViewmodelProvider,
            StudentMedicalViewModel.IProvider medicalViewModelProvider)
        {
            _query = query;
            _httpClient = httpClient;
            _studentListViewmodelProvider = studentListViewmodelProvider;
            _medicalViewModelProvider = medicalViewModelProvider;
        }

        public IActionResult List(bool filterFirstYearStudents, bool filterLawStudents)
        {
            return View(_studentListViewmodelProvider.Provide(filterFirstYearStudents, filterLawStudents));
        }

        [HttpGet]
        public async Task<IActionResult> Medical(Guid studentId)
        {
            var student = _query.GetSingleOrDefault(new HasIdSpec<Student>(studentId));
            if (student is null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync($"medical/student/{student.Id}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var medicals = JsonConvert.DeserializeObject<List<MedicalReadDto>>(content);

            var model = _medicalViewModelProvider.Provide(student.Id, medicals);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMedical(Guid studentId, MedicalWriteDto writeDto)
        {
            var response = await _httpClient.PostAsync($"medical/student/{studentId}", writeDto);
            response.EnsureSuccessStatusCode();
            
            return RedirectToAction(nameof(Medical), new {studentId});
        }

        [HttpPost]
        //PUT
        public async Task<IActionResult> EditMedicalReason(Guid medicalId, Guid studentId, MedicalWriteDto updateMedical)
        {
            updateMedical.Reason = updateMedical.Reason + "*";
            var response = await _httpClient.PutAsync($"medical/{medicalId}", updateMedical);
            response.EnsureSuccessStatusCode();
            
            return RedirectToAction(nameof(Medical), new {studentId});
        }

        [HttpPost]
        //DELETE
        public async Task<IActionResult> MarkMedicalNotActive(Guid medicalId, Guid studentId)
        {
            var response = await _httpClient.DeleteAsync($"medical/{medicalId}");
            response.EnsureSuccessStatusCode();
            
            return RedirectToAction(nameof(Medical), new {studentId});
        }
    }
}
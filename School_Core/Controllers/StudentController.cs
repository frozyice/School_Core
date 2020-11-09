using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School_Core.API.DTOs;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;
using School_Core.Util.ModelState;
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
        [ImportModelState]
        public async Task<IActionResult> Medical(Guid studentId)
        {
            var student = _query.GetSingleOrDefault(new HasIdSpec<Student>(studentId));
            if (student is null)
            {
                throw new ArgumentException(nameof(studentId));
            }

            var response = await _httpClient.GetAsync($"medical/student/{student.Id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("alert", "Request failed successfully!");
                return View(_medicalViewModelProvider.Provide(student.Id, new List<MedicalReadDto>(), false));
            }
            var content = await response.Content.ReadAsStringAsync();
            var medicals = JsonConvert.DeserializeObject<List<MedicalReadDto>>(content);
            var isRedirectedWithSuccess = false;
            if (TempData.ContainsKey("redirectWithSuccess"))
            {
                isRedirectedWithSuccess = (bool) TempData["redirectWithSuccess"];
            }
            var model = _medicalViewModelProvider.Provide(student.Id, medicals, isRedirectedWithSuccess);
            return View(model);
        }

        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> AddMedical(Guid studentId, MedicalWriteDto writeDto)
        {
            var student = _query.GetSingleOrDefault(new HasIdSpec<Student>(studentId));
            if (student is null)
            {
                throw new ArgumentException(nameof(studentId));
            }
            
            var response = await _httpClient.PostAsync($"medical/student/{student.Id}", writeDto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("alert", "Request failed successfully!");
                return RedirectToAction(nameof(Medical), new {studentId = student.Id});
            }
            TempData.Add("redirectWithSuccess", true);
            return RedirectToAction(nameof(Medical), new {studentId = student.Id});
        }

        [HttpPost]
        [ExportModelState]
        //PUT
        public async Task<IActionResult> EditMedicalReason(Guid medicalId, Guid studentId, MedicalWriteDto updateMedical)
        {
            var student = _query.GetSingleOrDefault(new HasIdSpec<Student>(studentId));
            if (student is null)
            {
                throw new ArgumentException(nameof(studentId));
            }
            
            updateMedical.Reason = updateMedical.Reason + "*";
            var response = await _httpClient.PutAsync($"medical/{medicalId}", updateMedical);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException(nameof(medicalId));
            }
            
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("alert", "Request failed successfully!");
                return RedirectToAction(nameof(Medical), new {studentId = student.Id});
            }
            TempData.Add("redirectWithSuccess", true);
            return RedirectToAction(nameof(Medical), new {studentId = student.Id});
        }

        [HttpPost]
        [ExportModelState]
        //DELETE
        public async Task<IActionResult> MarkMedicalNotActive(Guid medicalId, Guid studentId)
        {
            var student = _query.GetSingleOrDefault(new HasIdSpec<Student>(studentId));
            if (student is null)
            {
                throw new ArgumentException(nameof(studentId));
            }
            
            var response = await _httpClient.DeleteAsync($"medical/{medicalId}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException(nameof(medicalId));
            }
            
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("alert", "Request failed successfully!");
                return RedirectToAction(nameof(Medical), new {studentId = student.Id});
            }
            TempData.Add("redirectWithSuccess", true);
            return RedirectToAction(nameof(Medical), new {studentId = student.Id});
        }
    }
}
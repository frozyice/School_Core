using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School_Core.API.DTOs;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.ViewModels.Students;

namespace School_Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly IQuery<Student> _query;
        private readonly StudentListViewModel.IProvider _studentListViewmodelProvider;
        private readonly StudentMedicalViewModel.IProvider _medicalViewModelProvider;

        public StudentController(IQuery<Student> query, StudentListViewModel.IProvider studentListViewmodelProvider, StudentMedicalViewModel.IProvider medicalViewModelProvider)
        {
            _query = query;
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

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:3001/api/medical/student/{student.Id}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var medicals = JsonConvert.DeserializeObject<List<MedicalReadDto>>(content);

            var model = _medicalViewModelProvider.Provide(student.Id, medicals);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMedical(Guid studentId, MedicalWriteDto writeDto)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:3001/api/medical/student/{studentId}");
            var serialisedContent = JsonConvert.SerializeObject(writeDto);
            request.Content = new StringContent(serialisedContent);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Medical), new {studentId});
        }

        [HttpPost]
        //PUT
        public async Task<IActionResult> EditMedicalReason(Guid medicalId, Guid studentId, MedicalWriteDto updateMedical)
        {
            updateMedical.Reason = updateMedical.Reason + "*";
            var serialisedContent = JsonConvert.SerializeObject(updateMedical);
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:3001/api/medical/{medicalId}");
            var httpClient = new HttpClient();
            request.Content = new StringContent(serialisedContent);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responce = await httpClient.SendAsync(request);
            responce.EnsureSuccessStatusCode();
            
            return RedirectToAction(nameof(Medical), new {studentId});
        }

        [HttpPost]
        //DELETE
        public async Task<IActionResult> MarkMedicalNotActive(Guid medicalId, Guid studentId)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:3001/api/medical/{medicalId}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Medical), new {studentId});
        }
    }
}
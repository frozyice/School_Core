using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var response = await httpClient.GetAsync("https://localhost:3001/api/medical");
            
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var medicals = JsonConvert.DeserializeObject<List<MedicalGetDto>>(content);

            var model = _medicalViewModelProvider.Provide(student.Id, medicals);
            return View(model);
            //GET
        }

        public async Task<IActionResult> AddMedical()
        {
            throw new NotImplementedException();
            //POST
        }

        public async Task<IActionResult> EditMedicalReason()
        {
            throw new NotImplementedException();
            //PUT
        }

        public async Task<IActionResult> MarkMedicalNotActive()
        {
            throw new NotImplementedException();
            //DELETE
        }
    }
    
}
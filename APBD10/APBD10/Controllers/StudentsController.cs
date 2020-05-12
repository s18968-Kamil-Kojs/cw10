using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD10.DTOs.Requests;
using APBD10.Models;
using APBD10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD10.Controllers {

    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase {
        private IDbService _service;

        public StudentsController(IDbService service) {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetStudents() {
            return Ok(_service.GetStudents());
        }

        [HttpPut]
        public IActionResult ModifyStudent(Student student) {
            int returnCode = _service.ModifyStudent(student);
            if (returnCode == 200) return Ok("Dane studenta zostaly zmienione");
            else return BadRequest("Wystapil blad");
        }

        [HttpDelete]
        public IActionResult DeleteStudent(DeleteStudentRequest deleteStudentRequest) {
            int returnCode = _service.DeleteStudent(deleteStudentRequest);
            if (returnCode == 200) return Ok("Student zostal usuniety");
            else return BadRequest("Wystapil blad");
        }

        [HttpPost]
        [Route("api/students/enrollStudent")]
        public IActionResult SignUpStudentForStudies(EnrollStudentRequest request) {
            string response = _service.enrollStudent(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/students/promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request) {
            List<Enrollment> list = _service.promoteStudents(request);
            return Ok(list);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using APBD10.DTOs.Requests;
using APBD10.Models;
using Microsoft.AspNetCore.Http;

namespace APBD10.Services {

    public interface IDbService {

        public ICollection GetStudents();
        public int ModifyStudent(Student student);
        public int DeleteStudent(DeleteStudentRequest deleteStudentRequest);
        public string enrollStudent(EnrollStudentRequest request);
        public List<Enrollment> promoteStudents(PromoteStudentsRequest request);
    }
}

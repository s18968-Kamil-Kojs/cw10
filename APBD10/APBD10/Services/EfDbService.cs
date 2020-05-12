using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using APBD10.DTOs.Requests;
using APBD10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Services {

    public class EfDbService : IDbService{
        public s18968Context _context;

        public EfDbService(s18968Context context) {
            this._context = context;
        }

        public ICollection GetStudents() {
            var list = _context.Student.ToList();
            return list;
        }

        public int ModifyStudent(Student student) {
            var stud = _context.Student.Where(stud => stud.IndexNumber.Equals(student.IndexNumber)).ToList();
            if (stud.Count() == 0) return 300;
            stud.First().FirstName = student.FirstName;
            stud.First().LastName = student.LastName;
            stud.First().BirthDate = student.BirthDate;
            stud.First().IdEnrollment = student.IdEnrollment;
            _context.SaveChanges();
            return 200;
        }

        public int DeleteStudent(DeleteStudentRequest deleteStudentRequest) {
            var stud = _context.Student.Where(stud => stud.IndexNumber.Equals(deleteStudentRequest.Id)).ToList();
            if(stud.Count() == 0) return 300;
            _context.Remove(stud.First());
            _context.SaveChanges();
            return 200;
        }

        public string enrollStudent(EnrollStudentRequest request) {
            //Check if studies exist
            var studies = _context.Studies.Where(stud => stud.Name.Equals(request.Studies)).ToList();
            if (studies.Count() == 0) return "Studia nie istnieja";

            //Check is student's id is unique
            var student = _context.Student.Where(stud => stud.IndexNumber.Equals(request.IndexNumber)).ToList();
            if (student.Count() != 0) return "Student o podanym indeksie juz istieje";

            //Check if enrollment exists
            var enrollment = _context.Enrollment.Where(enrollm => enrollm.IdStudy.Equals(studies.First().IdStudy) && enrollm.Semester == 1).ToList();
            int idEnroll;

            //Enrollment doent exist
            if(enrollment.Count() == 0) {
                //get new max enrollment id
                var enrollments = _context.Enrollment.OrderByDescending(c => c.IdEnrollment).ToList();
                int maxId = enrollments.First().IdEnrollment;
                maxId++;

                //create new enrollment
                Enrollment newEnrollment = new Enrollment {
                    IdEnrollment = maxId,
                    Semester = 1,
                    IdStudy = studies.First().IdStudy,
                    StartDate = DateTime.Now
                };
                //add enrollment to EF context
                _context.Enrollment.Add(newEnrollment);
                _context.SaveChanges();

                idEnroll = maxId;
            } else {
                idEnroll = enrollment.First().IdEnrollment;
            }

            //Create new Student
            Student newStudent = new Student {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = Convert.ToDateTime(request.BirthDate),
                IdEnrollment = idEnroll
            };

            _context.Student.Add(newStudent);
            _context.SaveChanges();
            return "Student dodany";
        }

        public List<Enrollment> promoteStudents(PromoteStudentsRequest request) {
            _context.Database.ExecuteSqlRaw("EXEC PromoteStudents @Studies, @Semester", request.Studies, request.Semester);
            var list = _context.Enrollment.ToList();
            return list;
        }
    }
}

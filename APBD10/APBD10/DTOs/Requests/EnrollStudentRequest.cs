﻿using System;
using System.ComponentModel.DataAnnotations;

namespace APBD10.DTOs.Requests {

    public class EnrollStudentRequest {
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public string Studies { get; set; }

        public EnrollStudentRequest() {
        }
    }
}

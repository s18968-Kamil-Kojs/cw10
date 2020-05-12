﻿using System;
using System.ComponentModel.DataAnnotations;

namespace APBD10.DTOs.Requests {

    public class PromoteStudentsRequest {
        [Required]
        public string Studies { get; set; }
        [Required]
        public string Semester { get; set; }

        public PromoteStudentsRequest() {
        }
    }
}

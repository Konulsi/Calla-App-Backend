﻿using CallaApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Team
{
    public class TeamCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public string Testimonial { get; set; }
        public int PositionId { get; set; }
        //public List<TeamSocial> Icons { get; set; }
        public List<string> Link { get; set; }
    }
}
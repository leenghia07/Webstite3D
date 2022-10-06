using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }
    [Display(Name = "Nơi sinh")]
    public string? PlaceOfBirth { get; set; }
    [Display(Name = "Giới tính")]
    public string? Sex { get; set; }
    [Display(Name = "Hình ảnh")]
    public string? Image { get; set; }
    [Display(Name = "Họ")]
    [Required(ErrorMessage = "Chưa nhập họ")]
    public string FisrtName { get; set; }
    [Display(Name = "Tên")]
    [Required(ErrorMessage = "Chưa nhập tên")]
    public string LastName { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy:0}")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")]
    public DateTime BirthDate { get; set; }
}


using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TMS.application.DTOs;

public class UploadEventRequest
{
    [Required]
    public IFormFile File { get; set; }
}
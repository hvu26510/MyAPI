using Microsoft.AspNetCore.Mvc;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        public ImagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        private readonly IWebHostEnvironment _environment;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Kiểm tra nếu không có file nào được tải lên
            if (file == null || file.Length == 0)
            {
                return BadRequest("không có file nào được tải lên.");
            }
            //danh sách các định dạng file được phép tải lên
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            // Folder path để lưu trữ ảnh
            var folderPath = Path.Combine(_environment.WebRootPath, "images");

            //Xử lý file tải lên
            var extension  = Path.GetExtension(file.FileName).ToLower();
            
            var fileName = Guid.NewGuid().ToString() + extension;

            var filePath = Path.Combine(folderPath, fileName);
            
            // Kiểm tra nếu extension của file không hợp lệ
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Định dạng file không hợp lệ. Vui lòng tải lên một file ảnh.");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            

           
            return Ok("File đã được tải lên thành công.");

        }
    }
}
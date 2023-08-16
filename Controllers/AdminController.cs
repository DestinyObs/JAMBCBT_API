//using Microsoft.AspNetCore.Mvc;
//using JAMBAPI.Models;
//using JAMBAPI.ViewModels.DTOs;
//using JAMBAPI.Repositories;
//using Microsoft.AspNetCore.Authorization;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;

//namespace JAMBAPI.Controllers
//{
//    [Authorize(Roles = "SuperAdmin")]
//    [Route("api/admin")]
//    [ApiController]
//    public class AdminController : ControllerBase
//    {
//        private readonly IAdminRepository _adminRepository;
//        private readonly UserManager<Admin> _userManager;
//        private readonly SignInManager<Admin> _signInManager;
//        private readonly IJwtService _jwtService;

//        public AdminController(
//            IAdminRepository adminRepository,
//            UserManager<Admin> userManager,
//            SignInManager<Admin> signInManager,
//            IJwtService jwtService)
//        {
//            _adminRepository = adminRepository;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _jwtService = jwtService;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegistrationDto adminRegistrationDto)
//        {
//            // Validate the DTO and perform necessary checks

//            var admin = new Admin
//            {
//                Email = adminRegistrationDto.Email,
//                // Hash the password using a secure method before storing it
//                // Set other properties as needed
//            };

//            var result = await _userManager.CreateAsync(admin, adminRegistrationDto.Password);

//            if (result.Succeeded)
//            {
//                await _userManager.AddToRoleAsync(admin, "Admin");
//                return Ok(new { Message = "Admin account created successfully." });
//            }

//            return BadRequest(result.Errors);
//        }

//        [HttpPut("approve/{adminId}")]
//        public async Task<IActionResult> ApproveAdmin(int adminId, [FromBody] AdminApprovalDto adminApprovalDto)
//        {
//            var admin = await _adminRepository.GetAdminByIdAsync(adminId);

//            if (admin == null)
//            {
//                return NotFound();
//            }

//            admin.IsApproved = adminApprovalDto.IsApproved;
//            await _adminRepository.UpdateAdminAsync(admin);

//            return Ok(new { Message = "Admin approval status updated successfully." });
//        }

//        [HttpGet("{adminId}")]
//        public async Task<IActionResult> GetAdminById(int adminId)
//        {
//            var admin = await _adminRepository.GetAdminByIdAsync(adminId);

//            if (admin == null)
//            {
//                return NotFound();
//            }

//            var adminDto = new AdminDto
//            {
//                Id = admin.Id,
//                Email = admin.Email,
//                IsApproved = admin.IsApproved,
//                // Other properties as needed
//            };

//            return Ok(adminDto);
//        }

//        // Implement other CRUD operations for admin accounts

//        [AllowAnonymous]
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] AdminLoginDto adminLoginDto)
//        {
//            // Validate the DTO and perform necessary checks

//            var result = await _signInManager.PasswordSignInAsync(adminLoginDto.Email, adminLoginDto.Password, false, lockoutOnFailure: false);

//            if (result.Succeeded)
//            {
//                var token = _jwtService.GenerateAdminToken(adminLoginDto.Email);

//                return Ok(new { Token = token });
//            }

//            return Unauthorized();
//        }
//    }
//}

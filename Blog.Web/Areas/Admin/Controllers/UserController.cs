using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Users;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<AppUser> _validator;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IImageHelper _imageHelper;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<AppUser> userManager, IValidator<AppUser> validator, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IImageHelper imageHelper, IMapper mapper, IUnitOfWork unitOfWork, IToastNotification toast)
        {
            _userManager = userManager;
            _validator = validator;
            _roleManager = roleManager;
            _mapper = mapper;
            _toast = toast;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var map = _mapper.Map<List<UserDto>>(users);

            foreach (var userDto in map)
            {
                var findUser = await _userManager.FindByIdAsync(userDto.Id.ToString());
                var role = string.Join("", await _userManager.GetRolesAsync(findUser));
                userDto.Role = role;
            }
            return View(map);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(new UserAddDto { Roles = roles });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            var map = _mapper.Map<AppUser>(userAddDto);
            var roles = await _roleManager.Roles.ToListAsync();
            var validation = await _validator.ValidateAsync(map);

            if (ModelState.IsValid)
            {
                map.UserName = userAddDto.Email;
                var result = await _userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDto.Password) ? "" : userAddDto.Password);
                if (result.Succeeded)
                {
                    var findRole = await _roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
                    await _userManager.AddToRoleAsync(map, findRole.ToString());
                    _toast.AddSuccessToastMessage(Messages.User.Add(userAddDto.Email), new ToastrOptions { Title = "Başarılı" });

                    return RedirectToAction("Index", "User", new { area = "Admin" });
                }
                else
                {
                    result.AddToIdentityModelState(this.ModelState);
                    validation.AddToModelState(this.ModelState);
                    return View(new UserAddDto { Roles = roles });
                }
            }
            return View(new UserAddDto { Roles = roles });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var roles = await _roleManager.Roles.ToListAsync();

            var map = _mapper.Map<UserUpdateDto>(user);
            map.Roles = roles;
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());

            if (user != null)
            {
                var userRole = string.Join("", await _userManager.GetRolesAsync(user));
                var roles = await _roleManager.Roles.ToListAsync();
                if (ModelState.IsValid)
                {
                    var map = _mapper.Map(userUpdateDto, user);
                    var validation = await _validator.ValidateAsync(map);
                    if (validation.IsValid)
                    {
                        user.UserName = userUpdateDto.Email;
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            await _userManager.RemoveFromRoleAsync(user, userRole);
                            var findRole = await _roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                            await _userManager.AddToRoleAsync(user, findRole.Name);
                            _toast.AddSuccessToastMessage(Messages.User.Update(userUpdateDto.Email), new ToastrOptions { Title = "Başarılı" });
                            return RedirectToAction("Index", "User", new { area = "Admin" });
                        }
                        else
                        {
                            result.AddToIdentityModelState(this.ModelState);

                            return View(new UserUpdateDto { Roles = roles });
                        }
                    }
                    else
                    {
                        validation.AddToModelState(this.ModelState);
                        return View(new UserUpdateDto { Roles = roles });
                    }

                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Delete(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _toast.AddSuccessToastMessage(Messages.User.Delete(user.UserName), new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            else
            {
                result.AddToIdentityModelState(this.ModelState);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var getImage = await _unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == user.Id, x => x.Image);
            var map = _mapper.Map<UserProfileDto>(user);
            map.Image.FileName = getImage.Image?.FileName;

            return View(map);
        }


        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var isVerified = await _userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
                // Şifre değişikliği isteniyorsa ve yeni şifre boş değilse
                if (isVerified && userProfileDto.NewPassword != null && userProfileDto.Photo != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                    // Şifre değişikliği başarılı ise ChangePasswordAsync metodu hem şifreyi hem de güvenlik damgasını (security stamp) günceller
                    if (result.Succeeded)
                    {
                        // Hem şifre hem de profil bilgileri güncelleniyor
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);

                        user.FirstName = userProfileDto.FirstName;
                        user.LastName = userProfileDto.LastName;
                        user.PhoneNumber = userProfileDto.PhoneNumber;

                        var imageUpload = await _imageHelper.Upload($"{userProfileDto.FirstName} {userProfileDto.LastName}", userProfileDto.Photo, ImageType.User);
                        Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, user.Email);
                        await _unitOfWork.GetRepository<Image>().AddAsync(image);
                        user.ImageId = image.Id;

                        await _userManager.UpdateAsync(user);

                        await _unitOfWork.SaveChangesAsync();

                        _toast.AddSuccessToastMessage("Şifreniz ve bilgilerini başarıyla değiştirildi.");
                        return View();
                    }
                    else
                    {
                        // Şifre değişikliği başarısız ise hata mesajlarını ekle
                        result.AddToIdentityModelState(ModelState);
                        return View();
                    }
                }
                else if (isVerified && userProfileDto.Photo != null)
                {
                    // Sadece profil bilgileri güncelleniyor
                    await _userManager.UpdateSecurityStampAsync(user);
                    user.FirstName = userProfileDto.FirstName;
                    user.LastName = userProfileDto.LastName;
                    user.PhoneNumber = userProfileDto.PhoneNumber;

                    var imageUpload = await _imageHelper.Upload($"{userProfileDto.FirstName} {userProfileDto.LastName}", userProfileDto.Photo, ImageType.User);
                    Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, user.Email);
                    await _unitOfWork.GetRepository<Image>().AddAsync(image);
                    user.ImageId = image.Id;

                    await _userManager.UpdateAsync(user);
                    await _unitOfWork.SaveChangesAsync();

                    _toast.AddSuccessToastMessage("Bilgileriniz başarıyla değiştirildi.");
                    return View();
                }
                else
                {
                    // Mevcut şifre yanlışsa hata mesajı göster
                    _toast.AddErrorToastMessage("Bilgileriniz güncellenirken hata oluştu");
                    return View();
                }
            }

            return View();
        }
    }
}

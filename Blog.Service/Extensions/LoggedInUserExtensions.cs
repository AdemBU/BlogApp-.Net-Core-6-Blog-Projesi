using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Extensions
{
    //JWT veya Identity ile giriş yapmış kullanıcının bilgilerini kolayca almak için oluşturulmuş bir extension method sınıfıdır. Yani: “Sisteme giriş yapan kullanıcının Id’sini ve e-mail adresini pratik şekilde almak” için kullanılıyor.
    public static class LoggedInUserExtensions
    {
        public static Guid GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            //Giriş yapan kullanıcının Id bilgisini JWT veya Cookie içindeki claim’den bulur.
            return Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));  //NameIdentifier, kullanıcının benzersiz kimliğini temsil eder. Identity’nin “UserId” tuttuğu claim'dir.
        }

        public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
        {
            //Kullanıcının JWT veya cookie içindeki Email claim’ini bulur.
            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
//ASP.NET Core’da bir kullanıcı giriş yaptığında ClaimsPrincipal içinde kullanıcı bilgileri taşınır.
//Bu bilgiler "Claims" adı verilen küçük parçalardır.

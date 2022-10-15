using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
//using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using QRCoder;

namespace GenerateQRCode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }
        public ActionResult GenerateQR()
        {
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode("Welcome to ALper", QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            Bitmap bitmap = code.GetGraphic(15);
            var bitmapBytes = ConvertBitmapToBytes(bitmap);
            return File(bitmapBytes,"images/jpeg");

        }

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
      
    }
}

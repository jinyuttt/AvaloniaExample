using Avalonia.Media.Imaging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAvalonia.Helper
{
   
        public static class QRCoderHelper
        {
            ///生成二维码
            /// </summary>
            /// <param name="TextContent">文本信息</param>
            /// <param name="level">容错等级</param>
            /// <param name="version">版本</param>
            /// <param name="pixel">像素点大小</param>
            /// <param name="darkColor">数点颜色</param>
            /// <param name="lightColor">背景颜色</param>
            /// <param name="iconPath">图标路径</param>
            /// <param name="iconSize">图标尺寸</param>
            /// <param name="iconBorder">图标边框厚度</param>
            /// <param name="whiteBorder">二维码白边</param>
            /// <returns></returns>
            public static Bitmap generateQrCode(string TextContent, string level, int version, int pixel, Color darkColor, Color lightColor, string iconPath, int iconSize, int iconBorder, bool whiteBorder)
            {
            //       QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            //       QRCoder.QRCodeGenerator codeGenerator = new QRCoder.QRCodeGenerator();

            //       QRCoder.QRCodeData codeData = codeGenerator.CreateQrCode(TextContent, eccLevel, false, false, QRCoder.QRCodeGenerator.EciMode.Utf8, version);

            //   QRCoder.BitmapByteQRCode code = new BitmapByteQRCode(codeData);
            //   //  QRCoder. code = new QRCoder.QRCode(codeData);
            ////   QRCode qrCode = new QRCode(qrCodeData);
            //   if (iconPath == "")
            //       {
            //           Bitmap bmp = code.(pixel, darkColor, lightColor, whiteBorder);

            //           return bmp;
            //       }
            //       Bitmap icon = new Bitmap(iconPath);

            //       Bitmap iocnbmp = code.GetGraphic(pixel, darkColor, lightColor, icon, iconSize, iconBorder, whiteBorder);

            //       return iocnbmp;
            return null;
            }
        }

    
}

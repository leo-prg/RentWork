using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RentLibrary;



// Вспомогательные функции

namespace RentCommon
{
    public class Transform
    {
        //Из базы в картинку
        public static Image ByteToImage(byte[] bytesArr)
        {

            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        //Из картинки в базу
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        //Точки в ширину и высоту прямоугольника
        public static void PointsToDimensions(int x1, int y1, int x2, int y2, out int width, out int height)
        {

            width = Math.Abs(x1 - x2);
            height = Math.Abs(y1 - y2);

        }

        //Принадлежность точки к многоугольнику

        public static bool PointInPolygon(List<AreaPoint> polygon, AreaPoint point)
        {

            var intersects = new List<int>();
            var a = polygon.LastOrDefault();
            foreach (var b in polygon)
            {
                if (b.PointX == point.PointX && b.PointY == point.PointY)
                {
                    return true;
                }

                if (b.PointX == a.PointX && point.PointX == a.PointX && point.PointX >= Math.Min(a.PointY, b.PointY) && point.PointY <= Math.Max(a.PointY, b.PointY))
                {
                    return true;
                }

                if (b.PointY == a.PointY && point.PointY == a.PointY && point.PointX >= Math.Min(a.PointX, b.PointX) && point.PointX <= Math.Max(a.PointX, b.PointX))
                {
                    return true;
                }

                if ((b.PointY < point.PointY && a.PointY >= point.PointY) || (a.PointY < point.PointY && b.PointY >= point.PointY))
                {
                    var px = (int)(b.PointX + 1.0 * (point.PointY - b.PointY) / (a.PointY - b.PointY) * (a.PointX - b.PointX));
                    intersects.Add(px);
                }

                a = b;
            }

            intersects.Sort();
            return intersects.IndexOf(point.PointX) % 2 == 0 || intersects.Count(x => x < point.PointX) % 2 == 1;
        }

        //    public static bool PointInPolygon(List<AreaPoint> poly, AreaPoint point)
        //    {
        //        var coef = poly.Skip(1).Select((p, i) =>
        //                                        (point.PointY - poly[i].PointY) * (p.PointX - poly[i].PointX)
        //                                      - (point.PointX - poly[i].PointX) * (p.PointY - poly[i].PointY));

        //        var coefNum = coef.GetEnumerator();

        //        if (coef.Any(p => p == 0))
        //            return true;

        //        int lastCoef = coefNum.Current,
        //            count = coef.Count();

        //        coefNum.MoveNext();

        //        do
        //        {
        //            if (coefNum.Current - lastCoef < 0)
        //                return false;

        //            lastCoef = coefNum.Current;
        //        }
        //        while (coefNum.MoveNext());

        //        return true;
        //    }
        }

    public class PwdCrypt
        {
            private static readonly string rsaKey = "<RSAKeyValue><Modulus>u2Tc/r3+PCF/RJKzItIK0PvH6hI9Nm4JyVAceslyB/1ii1pHTzUycu+zEDPKGLwBf4zoxBDH9FoD5E3I+d0m3Ape+mzCcvexP4E5W6/kba4fWM5LFOCO+eSQmoR8nEsEI32QFOUjX5nsxVxahElxGRZVgdmQTLOaV626aShIRnE=</Modulus><Exponent>AQAB</Exponent><P>6ReHfDakGxwAevHkCFb/wQgh6JUdgOt/8MF9LCLepV/RQAaRn2mBC76Ghr6EVgKJt436FTTElBsV3rep9JHEmQ==</P><Q>zc+ZLtodrTYSCv/rqGZxrmnsY2UYOtQW0oezSQRuI7KwH5uPxsvE4RG0ELP1QYN2RG7VtPBdFYvuE2KPqv1fmQ==</Q><DP>0K+rMm9tP4Qzfd9hTIIvmnAgg641av3tXuysl75kXeVKX6tB51o7GfurT6n0q1i8WeU4GJJUyVDypKME+50iSQ==</DP><DQ>Ut5H+4lvcZLX6P3q0T+Ofn+/4rWN8AH9vn9NPRU/k7gbGl70oULi3wzVx/PtyJc6Q4utGNG/aTGShtOkrWCf4Q==</DQ><InverseQ>k4C24Kg3+dJEBj2VNz6Vrwjicq/idnWoL1+TzYliwSWs/Yai/oL5aaa1ZKCrVqqOJZdIz/yGcpQwrxvmbLD9Hg==</InverseQ><D>A2Rz+BGeRrOQNKss3oUWaIpHqCC3O83EqWtk70JyfnCzcL825MPGBhzO7Uy+txwU29yuwbk7DBz4DpFA7D25FxaP6CDbSRBI5Q0nJ608D7DJHXzxSVoxfe4rjIuQBA3fvwtTKYdjTegwyNX/4bevAEfrkfejp/EOFwvhvmfiJXE=</D></RSAKeyValue>";

            public static string Encrypt(string pwd)
            {
                try
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(rsaKey);
                    return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(pwd), true));
                }
                catch { }
                return "";
            }

            public static string Decrypt(string pwd)
            {
                try
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(rsaKey);
                    return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(pwd), true));
                }
                catch { }
                return "";
            }


        }

    public static class Finance
    {
        // Определение суммы без НДС
        public static float WithoutVat(float amount)
        {
            float withoutvat;
            withoutvat = amount - (amount * 20 / 120);
            return withoutvat;
        }
    }
 }



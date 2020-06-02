using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SD = System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Drawing;

namespace RIS
{
    public partial class ImageCrop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request.Params["category"] == "cropimage")
            {
                string Imagefilename =Path.GetFileName(Context.Request.Params["imagepath"].ToString());
                int w = Convert.ToInt32(Context.Request.Params["Width"].ToString());
                int h = Convert.ToInt32(Context.Request.Params["Height"].ToString());
                int x = Convert.ToInt32(Context.Request.Params["chordx"].ToString());
                int y = Convert.ToInt32(Context.Request.Params["chordy"].ToString());
                string path = Context.Server.MapPath("~/PackThumbnailImages/");
                byte[] CropImage = Crop(path+Imagefilename, w, h, x, y);
                using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
                {
                    ms.Write(CropImage, 0, CropImage.Length);
                    using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                    {
                        string SaveTo = path  + Path.GetFileNameWithoutExtension(Imagefilename)+"_thumb"+Path.GetExtension(Imagefilename);
                        CroppedImage.Save(SaveTo, CroppedImage.RawFormat);
                        Context.Response.Write("PackThumbnailImages/" + Path.GetFileNameWithoutExtension(Imagefilename) + "_thumb" + Path.GetExtension(Imagefilename)+"?"+"&date="+System.DateTime.Now);
                        Context.Response.End();
                        //pnlCrop.Visible = false;
                        //pnlCropped.Visible = true;
                        //imgCropped.ImageUrl = "images/crop" + ImageName;
                    }
                }
            }
            if (Context.Request.Params["category"] == "iefile")
            {
                var filename=Context.Request.Params["upfile"].ToString();
                FileInfo fi = new FileInfo(filename);
                byte[] image1 = GetFileBytes(fi);
                
                Int64 intDocFileLength = image1.Length;
                double intFileSize = 0;
                Int16 intFileMBSIze = 0;
                string msg = "{";
                if ((Convert.ToString(ConfigurationManager.AppSettings["SizeLimitinMB"]) != "") && (Convert.ToString(ConfigurationManager.AppSettings["SizeLimitinMB"]) != null))
                {
                    intFileSize = (1.049e+6) * Convert.ToInt16(ConfigurationManager.AppSettings["SizeLimitinMB"]);
                    intFileMBSIze = Convert.ToInt16(ConfigurationManager.AppSettings["SizeLimitinMB"]);
                }
                else
                {
                    intFileSize = (1024) * 20;
                    intFileMBSIze = 20;
                }

                //file size 5 MB
                if (intDocFileLength > intFileSize)
                {
                    msg += string.Format("error:'{0}',\n", "Image file size exceeds the limit of " + intFileMBSIze + " MB");
                    msg += string.Format("msg:'{0}'\n", String.Empty);
                    msg += "}";

                }
                else
                {
                    string path = Context.Server.MapPath("~/PackThumbnailImages/");
                  //  FileStream fs = fi.Create();
                    //var file = context.Request.Files[0];
                    System.Drawing.Image myImage = Bitmap.FromFile(fi.FullName);
                    var myheight = myImage.Height;
                    var mywidth = myImage.Width;
                    var maxheight = 500;
                    var maxwidth = 500;
                    if (myheight > maxheight)
                    {
                        myheight = maxheight;
                    }
                    if (mywidth > maxwidth)
                    {
                        mywidth = maxwidth;
                    }
                    Bitmap bmSave = new Bitmap(myImage, new Size(mywidth, myheight));
                    Bitmap bmTemp = new Bitmap(bmSave, new Size(mywidth, myheight));

                    System.Drawing.Graphics grSave = Graphics.FromImage(bmTemp);
                    grSave.DrawImage(myImage, 0, 0, mywidth, myheight);




                    string fileName;

                 
                    string strFileName = fi.Name;
                    fileName = Path.Combine(path, strFileName);
                    try
                    {

                        //   file.SaveAs(fileName);
                        bmTemp.Save(fileName);


                        myImage.Dispose();
                        bmSave.Dispose();
                        bmTemp.Dispose();
                        grSave.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }

                  //  msg += string.Format("error:'{0}',\n", String.Empty);
                    msg =  "PackThumbnailImages/" + Path.GetFileName(fi.FullName) + "~" + mywidth.ToString() + "~" + myheight.ToString();
                  //  msg += "}";


                }
                Context.Response.Write(msg);
                Context.Response.End();
            }
            if (Context.Request.Params["category"] == "sessioncheck")
            {
              //  var imagespath = Context.Session["UserId"].ToString();
                if (String.IsNullOrEmpty(Convert.ToString(Context.Session["UserId"])))
                {
                    Context.Response.Write("expired");
                    Context.Response.End();
                }
                else
                {
                    Context.Response.Write("alive");
                    Context.Response.End();
                }

            }

        }
        static byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (SD.Image OriginalImage = SD.Image.FromFile(Img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                          
                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        private static byte[] GetFileBytes(FileInfo f)
        {
            using (FileStream stream = f.OpenRead())
            {
                byte[] buffer = new byte[f.Length];
                stream.Read(buffer, 0, (int)f.Length);
                return buffer;
            }
        }
    }
}
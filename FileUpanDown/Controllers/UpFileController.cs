using FileUpanDown.TOOL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpanDown.Controllers
{
    /// <summary>
    /// 文件上传接口
    /// </summary>
    public class UpFileController:BaseAPIController
    {
        /// <summary>
        /// 文件上传（通过表单file上传）
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/Upfile/upload")]
        public async Task<IActionResult> UploadFileAsync()
        {
            FileHelper fileHelper = new FileHelper();//文件类
            string TransformName = fileHelper.CreateRandomstr(7); // 生成7位转换名称

            var form = Request.Form;
            if (form.Files.Count == 0) return Fail("未获取到文件");
            var video = form.Files[0];

            string ext = Path.GetExtension(video.FileName); // 扩展名(.jpg/mp3...)    
            //在这里可以填添加文件类型拦截            

            //在这里读取appsetting配置下载目录 
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string VideoPath = config["filePath"]; // 文件保存路径
            //文件夹不存在则创建文件夹
            if (!Directory.Exists(VideoPath))
            {
                Directory.CreateDirectory(VideoPath);
            }
            try
            {
                // 读文件
                var openReadStream = video.OpenReadStream();
                byte[] buff = new byte[openReadStream.Length];
                await openReadStream.ReadAsync(buff, 0, buff.Length);

                // 写文件
                VideoPath += TransformName + ext;//加后缀名
                using (FileStream fs = new FileStream(VideoPath, FileMode.Create))
                {
                    await fs.WriteAsync(buff, 0, buff.Length);
                }
                //在这里保存文件信息信息到数据库
                
                return Success(TransformName);
            }
            catch (Exception error)
            {
                return Fail("上传文件失败：" + error.Message);
            }
        }
    }
}

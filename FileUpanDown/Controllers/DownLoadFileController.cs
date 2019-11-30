using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpanDown.Controllers
{
    /// <summary>
    /// 下载接口
    /// </summary>
    public class DownLoadFileController:BaseAPIController
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/DownLoadfile/Download/{fileid}")]
        public async Task<IActionResult> DownLoadFile(string fileid)
        {
            string FilePath = @"E:\Desktop\sparkle\PIC\t1.jpg";//在这里通过fileid从数据库获取文件的地址
            var result = await Task.Run(() =>
            {
                var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);//读取文件
                string fileExt = Path.GetExtension(FilePath);//获取扩展名
                var provider = new FileExtensionContentTypeProvider();
                var memi = provider.Mappings[fileExt];//文件类型
                return File(stream, memi, Path.GetFileName(FilePath));
            });
            return result;
        }
    }
}

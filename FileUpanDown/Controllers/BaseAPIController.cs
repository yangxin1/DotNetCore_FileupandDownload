using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FileUpanDown.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public class BaseAPIController : ControllerBase
    {
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public virtual IActionResult Success(dynamic Data=null)
        {
             return new JsonResult(new { code = 200, msg = "操作成功", data = Data });
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public virtual IActionResult Fail(string Msg,dynamic Data=null)
        {
            return new JsonResult(new { code = 200, msg = Msg, data= Data});
        }
    }
}
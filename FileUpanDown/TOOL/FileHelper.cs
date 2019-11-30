using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpanDown.TOOL
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CreateRandomstr(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int num = random.Next(65, 91);
                string abc = Convert.ToChar(num).ToString();
                sb.Append(abc);
            }
            return sb.ToString();
        }
    }
}

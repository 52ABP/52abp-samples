using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using ExcelImportDemo.Authentication.External;
using ExcelImportDemo.Authentication.JwtBearer;
using ExcelImportDemo.Authorization;
using ExcelImportDemo.Authorization.Users;
using ExcelImportDemo.Models.TokenAuth;
using ExcelImportDemo.MultiTenancy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Data;
using SysFile = System.IO.File;
using System.Net.Http.Headers;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using Abp.AspNetCore.Mvc.Authorization;
using ExcelImportDemo.Authorization.Roles;

namespace ExcelImportDemo.Controllers
{
    [AbpMvcAuthorize]
    [Route("api/[controller]/[action]")]
    public class ExcelController : ExcelImportDemoControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public ExcelController(
           IHostingEnvironment hostingEnvironment,
           IPasswordHasher<User> passwordHasher,
           UserManager userManager,
           RoleManager roleManager
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost]
        public async Task<JsonResult> Import(ImportTypeEnum importType)
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                throw new Abp.UI.UserFriendlyException("请选择文件!");
            }

            switch (importType)
            {
                case ImportTypeEnum.User:
                    await ImportAdpter(file, ImportUserSample);
                    break;
                case ImportTypeEnum.Role:
                    await ImportAdpter(file, ImportRoleSample);
                    break;
                default:
                    throw new Abp.UI.UserFriendlyException("导入类型错误!");
            }

            return Json(true);
        }

        /// <summary>
        /// 导入角色
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private async Task ImportRoleSample(DataTable dataTable)
        {
            Role newRole = null;
            foreach (DataRow row in dataTable.Rows)
            {
                newRole = new Role();
                newRole.Name = row[0].ToString();
                newRole.DisplayName = row[1].ToString();
                newRole.Description = row[2].ToString();

                await _roleManager.CreateAsync(newRole);
            }

        }

        /// <summary>
        /// 导入用户
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private async Task ImportUserSample(DataTable dataTable)
        {
            User newUser = null;
            foreach (DataRow row in dataTable.Rows)
            {
                newUser = new User();
                newUser.UserName = row[0].ToString();
                newUser.Name = newUser.UserName;
                newUser.Surname = newUser.UserName;

                newUser.EmailAddress = row[1].ToString();

                newUser.Password = _passwordHasher.HashPassword(newUser, row[2].ToString());

                await _userManager.CreateAsync(newUser);
            }
        }



        #region 导入适配器

        /// <summary>
        /// 导入适配器函数
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="importFunc">导入的方法</param>
        /// <param name="startIndex">开始的行数</param>
        /// <returns></returns>
        private async Task ImportAdpter(IFormFile file, Func<DataTable, Task> importFunc, int startIndex = 0)
        {
            var filePath = string.Empty;
            try
            {
                this.SaveFile(file, out string orginalFileName, out string fileName, out filePath);
            }
            catch (Exception e)
            {
                throw new Abp.UI.UserFriendlyException("创建临时文件失败！");
            }

            try
            {
                // 将excel文件转换成datatable
                var dataTable = ExcelFileToDataTable(filePath, startIndex);
                if (dataTable != null && dataTable != null && importFunc != null)
                {
                    // 导入数据
                    await importFunc(dataTable);
                }
            }
            catch (Exception e)
            {
                throw new Abp.UI.UserFriendlyException("导入适配数据出错！" + e.Message);
            }
            finally// 删除文件
            {
                if (SysFile.Exists(filePath))
                {
                    SysFile.Delete(filePath);
                }
            }
        }

        #endregion


        #region Excel文件保存到本地

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private void SaveFile(IFormFile file, out string originalFileName, out string fileName, out string filePath)
        {
            originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            this.SaveMedia(file.OpenReadStream(), fileName, out filePath, file.ContentType);
        }

        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="mediaBinaryStream"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        private void SaveMedia(Stream mediaBinaryStream, string fileName, out string filePath, string mimeType = null)
        {
            var dirPath = Path.Combine(_hostingEnvironment.WebRootPath, "tmp");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            filePath = Path.Combine(dirPath, fileName).Replace("\\", "/");

            using (var output = new FileStream(filePath, FileMode.Create))
            {
                mediaBinaryStream.CopyTo(output);
            }
        }

        #endregion




        #region Excel文件读取到DataTable


        /// <summary>
        /// 获取excel内容
        /// </summary>
        /// <param name="filePath">excel文件路径</param>
        /// <returns></returns>
        protected static DataTable ExcelFileToDataTable(string filePath, int startIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream fsRead = System.IO.File.OpenRead(filePath))
            {
                IWorkbook wk = null;
                //获取后缀名
                string extension = filePath.Substring(filePath.LastIndexOf(".")).ToString().ToLower();
                //判断是否是excel文件
                if (extension == ".xlsx" || extension == ".xls")
                {
                    //判断excel的版本
                    if (extension == ".xlsx")
                    {
                        wk = new XSSFWorkbook(fsRead);
                    }
                    else
                    {
                        wk = new HSSFWorkbook(fsRead);
                    }

                    //获取第一个sheet
                    ISheet sheet = wk.GetSheetAt(0);
                    //获取第一行
                    IRow headrow = sheet.GetRow(0);


                    //创建列
                    for (int i = 0; i < headrow.Cells.Count; i++)
                    {
                        DataColumn datacolum = new DataColumn("F" + (i + 1));
                        dt.Columns.Add(datacolum);
                    }
                    //读取每行,从第二行 第一列开始
                    for (int r = 1; r <= sheet.LastRowNum; r++)
                    {
                        bool result = false;
                        DataRow dr = dt.NewRow();
                        //获取当前行
                        IRow row = sheet.GetRow(r);
                        if (row == null)
                        {
                            continue;
                        }
                        //读取每列
                        for (int j = startIndex; j < row.Cells.Count; j++)
                        {
                            ICell cell = row.GetCell(j); //一个单元格
                            dr[j - startIndex] = GetCellValue(cell); //获取单元格的值
                                                                     //全为空则不取
                            if (dr[j - startIndex].ToString() != "")
                            {
                                result = true;
                            }
                        }
                        if (result == true)
                        {
                            dt.Rows.Add(dr); //把每行追加到DataTable
                        }
                    }
                }

            }
            return dt;
        }

        //对单元格进行判断取值
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;

            switch (cell.CellType)
            {
                case CellType.Blank: //空数据类型 这里类型注意一下，不同版本NPOI大小写可能不一样,有的版本是Blank（首字母大写)
                    return string.Empty;
                case CellType.Boolean: //bool类型
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric: //数字类型
                    if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else //其它数字
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.Unknown: //无法识别类型
                default: //默认类型
                    return cell.ToString();//
                case CellType.String: //string 类型
                    return cell.StringCellValue;
                case CellType.Formula: //带公式类型
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }


        protected MemoryStream RenderToExcel(DataTable table)
        {
            MemoryStream ms = new MemoryStream();

            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);


                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        sheet.SetColumnWidth(i, 60 * 256);
                    }
                    else if (i == 0 || i == 4 || i == 5)
                    {
                        sheet.SetColumnWidth(i, 30 * 256);
                    }
                }


                foreach (DataColumn column in table.Columns)
                {
                    // 如果标题未设置，则返回ColumnName值
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                }


                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in table.Columns)
                    {
                        var cell = dataRow.CreateCell(column.Ordinal);
                        cell.SetCellValue(row[column].ToString());
                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        #endregion
    }


    public enum ImportTypeEnum
    {
        User = 1,
        Role = 2
    }
}

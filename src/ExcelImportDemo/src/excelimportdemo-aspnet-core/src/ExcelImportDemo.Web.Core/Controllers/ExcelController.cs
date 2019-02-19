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
                throw new Abp.UI.UserFriendlyException("��ѡ���ļ�!");
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
                    throw new Abp.UI.UserFriendlyException("�������ʹ���!");
            }

            return Json(true);
        }

        /// <summary>
        /// �����ɫ
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
        /// �����û�
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



        #region ����������

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="file">�ļ�</param>
        /// <param name="importFunc">����ķ���</param>
        /// <param name="startIndex">��ʼ������</param>
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
                throw new Abp.UI.UserFriendlyException("������ʱ�ļ�ʧ�ܣ�");
            }

            try
            {
                // ��excel�ļ�ת����datatable
                var dataTable = ExcelFileToDataTable(filePath, startIndex);
                if (dataTable != null && dataTable != null && importFunc != null)
                {
                    // ��������
                    await importFunc(dataTable);
                }
            }
            catch (Exception e)
            {
                throw new Abp.UI.UserFriendlyException("�����������ݳ���" + e.Message);
            }
            finally// ɾ���ļ�
            {
                if (SysFile.Exists(filePath))
                {
                    SysFile.Delete(filePath);
                }
            }
        }

        #endregion


        #region Excel�ļ����浽����

        /// <summary>
        /// �����ļ�
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
        /// д�뵽�ļ�
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




        #region Excel�ļ���ȡ��DataTable


        /// <summary>
        /// ��ȡexcel����
        /// </summary>
        /// <param name="filePath">excel�ļ�·��</param>
        /// <returns></returns>
        protected static DataTable ExcelFileToDataTable(string filePath, int startIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream fsRead = System.IO.File.OpenRead(filePath))
            {
                IWorkbook wk = null;
                //��ȡ��׺��
                string extension = filePath.Substring(filePath.LastIndexOf(".")).ToString().ToLower();
                //�ж��Ƿ���excel�ļ�
                if (extension == ".xlsx" || extension == ".xls")
                {
                    //�ж�excel�İ汾
                    if (extension == ".xlsx")
                    {
                        wk = new XSSFWorkbook(fsRead);
                    }
                    else
                    {
                        wk = new HSSFWorkbook(fsRead);
                    }

                    //��ȡ��һ��sheet
                    ISheet sheet = wk.GetSheetAt(0);
                    //��ȡ��һ��
                    IRow headrow = sheet.GetRow(0);


                    //������
                    for (int i = 0; i < headrow.Cells.Count; i++)
                    {
                        DataColumn datacolum = new DataColumn("F" + (i + 1));
                        dt.Columns.Add(datacolum);
                    }
                    //��ȡÿ��,�ӵڶ��� ��һ�п�ʼ
                    for (int r = 1; r <= sheet.LastRowNum; r++)
                    {
                        bool result = false;
                        DataRow dr = dt.NewRow();
                        //��ȡ��ǰ��
                        IRow row = sheet.GetRow(r);
                        if (row == null)
                        {
                            continue;
                        }
                        //��ȡÿ��
                        for (int j = startIndex; j < row.Cells.Count; j++)
                        {
                            ICell cell = row.GetCell(j); //һ����Ԫ��
                            dr[j - startIndex] = GetCellValue(cell); //��ȡ��Ԫ���ֵ
                                                                     //ȫΪ����ȡ
                            if (dr[j - startIndex].ToString() != "")
                            {
                                result = true;
                            }
                        }
                        if (result == true)
                        {
                            dt.Rows.Add(dr); //��ÿ��׷�ӵ�DataTable
                        }
                    }
                }

            }
            return dt;
        }

        //�Ե�Ԫ������ж�ȡֵ
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;

            switch (cell.CellType)
            {
                case CellType.Blank: //���������� ��������ע��һ�£���ͬ�汾NPOI��Сд���ܲ�һ��,�еİ汾��Blank������ĸ��д)
                    return string.Empty;
                case CellType.Boolean: //bool����
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric: //��������
                    if (HSSFDateUtil.IsCellDateFormatted(cell))//��������
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else //��������
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.Unknown: //�޷�ʶ������
                default: //Ĭ������
                    return cell.ToString();//
                case CellType.String: //string ����
                    return cell.StringCellValue;
                case CellType.Formula: //����ʽ����
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
                    // �������δ���ã��򷵻�ColumnNameֵ
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

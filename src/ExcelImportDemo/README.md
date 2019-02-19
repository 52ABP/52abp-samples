# 导入Excel文件数据 示例说明

* 使用52ABP Free基础模板构建 [创建项目](https://www.52abp.com/download)
* Excel文件处理使用DotNetCore.NPOI 

## 测试使用文件
`src/ExcelImportDemo/test_excel_files` 目录下
* `users.xls` excel导入测试用户文件
* `roles.xls` excel导入测试角色文件

## 后台
`src/ExcelImportDemo/src/excelimportdemo-aspnet-core/src/ExcelImportDemo.Web.Core` 项目
* 安装nuget包 `DotNetCore.NPOI `, 版本`v1.2.1`
* 新增控制器`ExcelController`, 并编写相关业务代码

## 前端
`src/ExcelImportDemo/src/excelimportdemo-aspnet-core/src/ExcelImportDemo.Web.Core` 目录

#### 公共类 ***RequestHelper***
`src/shared/helpers/RequestHelper.ts`
* 用于创建 `HttpClient` 请求头的类,附加身份认证信息和安全校验信息

#### 导入组件
`src/app/shared/components/excel-import/` 目录下
* 导入excel的组件

#### 用户/角色 列表界面
* `.ts`文件增加 `importExcel` 导入数据函数
* `.html`文件增加导入数据按钮

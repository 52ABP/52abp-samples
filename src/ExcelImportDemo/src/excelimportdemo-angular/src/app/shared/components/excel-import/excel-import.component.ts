import { Component, OnInit, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { HttpClient, HttpRequest, HttpResponse } from '@angular/common/http';
import { UploadFile } from 'ng-zorro-antd';
import { filter } from 'rxjs/operators';
import { AppConsts } from '@shared/AppConsts';
import { RequestHelper } from '@shared/helpers/RequestHelper';

@Component({
    selector: 'app-excel-import',
    templateUrl: './excel-import.component.html',
    styles: []
})
export class ExcelImportComponent extends ModalComponentBase implements OnInit {

    /**
     * 导入excel文件的api相对路径
     */
    excelImportUrl = "/api/Excel/Import"

    /**
     * 导入的类型
     */
    importType: number;
    /**
     * 文件支持的格式
     */
    accept = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel';
    /**
     * 上传状态
     */
    uploading = false;
    /**
     * 文件集合
     */
    fileList: UploadFile[] = [];

    constructor(injector: Injector,
        private _http: HttpClient,
    ) {
        super(injector);
    }

    ngOnInit() {

    }

    beforeUpload = (file: UploadFile): boolean => {
        this.fileList.push(file);
        return false;
    }


    save(): void {
        if (this.fileList.length < 1) {
            this.message.warn("请选择文件！");
            return;
        }

        this.uploading = true;

        // formdata拼接
        let formData = new FormData();
        formData.append('importType', this.importType + '');
        this.fileList.forEach((file: any) => {
            formData.append('files[]', file);
        });

        // 创建HTTP请求头
        var modifiedHeaders = RequestHelper.createHttpHeaders();

        let request = new HttpRequest('POST', AppConsts.remoteServiceBaseUrl + this.excelImportUrl, formData, {
            headers: modifiedHeaders
        });

        // 发送请求
        this._http
            .request(request)
            .pipe(filter(e => e instanceof HttpResponse))
            .subscribe(
                (event: {}) => {
                    this.uploading = false;
                    this.message.success('导入成功');
                    this.success();
                },
                err => {
                    debugger
                    let result = err.error;

                    this.uploading = false;
                    this.message.error(`导入失败！${result.error.message}`);
                }
            );
    }
}

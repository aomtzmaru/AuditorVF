import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FileItem, FileUploader } from 'ng2-file-upload';
import { RequestFile } from 'src/app/_models/requestFile';
import { AlertService } from 'src/app/_services/alert.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  @Input() requestFiles!: RequestFile[];
  @Output() sendRequestFiles = new EventEmitter<RequestFile[]>();

  fileUrl = environment.fileUrl;
  uploader!: FileUploader;
  hasBaseDropZoneOver!: false;
  baseUrl = environment.apiUrl;
  requestFile!: RequestFile;

  constructor(private alert: AlertService) { }

  ngOnInit(): any {
    this.initializeUpload();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUpload(): any {
    this.uploader = new FileUploader({
      isHTML5: true,
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 100 * 1024 * 1024,
      allowedFileType: ['image', 'video', 'audio', 'pdf', 'compress', 'doc', 'xls', 'ppt']
    });

    this.uploader.onAfterAddingFile = (file: any) => {
      file.withCredentials = false;
      this.createFileStream(file);
    };

  }

  clearAllFile(): void {
    this.requestFiles = [];
    this.uploader.clearQueue();
    this.sendRequestFiles.emit(this.requestFiles);
  }

  createFileStream(file: FileItem): void {
    const fileReader = new FileReader();
    fileReader.onloadend = (e) => {
      const fileData = fileReader.result;
      const rawData = fileData!.toString().split('base64,');

      let fStream = '';
      if (rawData.length > 1) {
        fStream = rawData[1];
      }
      this.requestFile = {
        fileStream: fStream,
        filePath: '',
        fileName: file.file.name,
        description: '',
        contentType: file.file.type,
      };
      this.requestFiles.push(this.requestFile);
    };
    fileReader.readAsDataURL(file._file);
    this.sendRequestFiles.emit(this.requestFiles);
  }

}

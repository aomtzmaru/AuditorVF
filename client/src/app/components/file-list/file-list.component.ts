import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { RequestFile } from 'src/app/_models/requestFile';
import { AlertService } from 'src/app/_services/alert.service';
import { DataService } from 'src/app/_services/data.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {

  @Input() requestFiles!: RequestFile[];
  @Input() mngMode!: boolean;
  @Output() sendF = new EventEmitter();
  @Output() delFileResult = new EventEmitter();

  fileUrl = environment.fileUrl;
  showData = false;
  baseUrl = environment.apiUrl;
  localRequestFiles!: RequestFile[];

  constructor(
    private dataService: DataService,
    private alert: AlertService,
    private router: Router
  ) { }

  ngOnInit(): void {
    setTimeout(() => {
      this.localRequestFiles = this.requestFiles;
    }, 1000);
  }

  delFile(id: number, fileName: string): void {
    this.alert.confirm(`ยืนยันการลบไฟล์ ${fileName} ?`, 'ยืนยันการลบไฟล์').then((r: any) => {
      if (r.isConfirmed) {
        this.dataService.delFileService(id).subscribe((res: any) => {
          this.localRequestFiles = this.localRequestFiles.filter(p => p.id !== id);
          this.delFileResult.emit(this.localRequestFiles);
          this.alert.success(`ลบไฟล์ ${fileName} แล้ว`);
        });
        console.log('deleted');
      }
    });

    // this.alert.confirm(`ยืนยันการลบไฟล์ ${fileName} ?`, () => {
    //   this.ngxService.start();
    //   this.dataService.delFileService(id).subscribe((res: any) => {
    //     this.ngxService.stop();
    //     this.localRequestFiles = this.localRequestFiles.filter(p => p.id !== id);
    //     this.delFileResult.emit(this.localRequestFiles);
    //     this.alert.success(`ลบไฟล์ ${fileName} แล้ว`);
    //   });
    // });
  }

  downloadFile(id: number, fileName: string): void {
    this.dataService.downloadFile(id, fileName).subscribe((res: any) => {
      window.open(this.fileUrl + res.file);
    });
    console.log('downloaded');
  }

}

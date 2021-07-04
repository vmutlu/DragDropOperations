import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { ApiUrl } from './constants/ApiUrl';
import { HttpEntityRepositoryService } from './services/http-entity-repository.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'DragDropUI';
  uploadedImages: any = [];

  constructor(
    private toastr: ToastrService,
    private service: HttpEntityRepositoryService<any>
  ) {}

  progress: any;
  message: any;
  files: File[] = [];

  formPost(event: any): FormData {
    const formData: FormData = new FormData();
    formData.append('formFile', event.addedFiles[0], event.addedFiles[0].name);
    return formData;
  }

  onSelect(event: any) {
    this.service.upload(ApiUrl.File_Upload, this.formPost(event)).subscribe(
      (data: any) => {
        console.log(data);
        if (data.success) {
          this.toastr.success(
            'Resim yükleme işleminiz başarılı bir şekilde gerçekleşmiştir.'
          );
          this.message =
            'Yüklediğiniz resime ' +
            data.message +
            ' bu yoldan erişebilirsiniz.';
          this.uploadedImages.push(environment.BASE_URL + data.message);
        }
        else{
          this.toastr.error(
            data.message
          );
        }
      },
      (errorResponse) => {
        if (errorResponse.error && errorResponse.error.message) {
          this.toastr.error(errorResponse.error);
        }
      }
    );
  }

  onRemove(event: any) {
    var imageModel = Object.assign({});
    imageModel.ImagePath = event.split('\\')[2];

    this.service.delete(ApiUrl.File_Delete, imageModel).subscribe(
      (data: any) => {
        if (data.success) {
          this.toastr.success(data.message);
          this.uploadedImages = [];
        }
      },
      (errorResponse) => {
        if (errorResponse.error && errorResponse.error.message) {
          this.toastr.error(errorResponse.error);
        }
      }
    );
  }
}

import { Component, ViewChild, ViewEncapsulation, Input, EventEmitter, Output } from "@angular/core";
import {
    UploadComponent as KendoUploadComponent, UploadEvent
} from "@progress/kendo-angular-upload";
import { FormGroup, FormControl } from "@angular/forms";

@Component({
  selector: "rs-upload",
  encapsulation: ViewEncapsulation.None,
  styleUrls: ["./upload.component.css"],
  templateUrl: "./upload.component.html"
})
export class UploadComponent {
  @Output() close: EventEmitter<any> = new EventEmitter();
  @Input() isActive = false;
  @ViewChild(KendoUploadComponent) private upl;
  uploadSaveUrl = "http://localhost:5000/api/values/addfile"; // should represent an actual API endpoint
  uploadRemoveUrl = "removeUrl"; // should represent an actual API endpoint
  downloadForm: FormGroup = new FormGroup({});

onUpload() {
    this.upl.uploadFiles();
}

onClose(): boolean {
  this.isActive = false;
  this.close.emit();
  return false;
}

uploadEventHandler(e: UploadEvent) {
    e.data = {
      description: "File upload"
    };
  }
}
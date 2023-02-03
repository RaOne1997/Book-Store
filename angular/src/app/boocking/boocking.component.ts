import { PageAlertService } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { Subscription, take } from 'rxjs';
import { CompressImageService } from './compressImageServices';
import { PostService } from './post.service';
import { TicketService } from './ticketservice';
@Component({
  selector: 'app-boocking',
  templateUrl: './boocking.component.html',
  styleUrls: ['./boocking.component.scss'],
  providers: [MessageService, TicketService]
})
export class BoockingComponent implements OnInit {
  // POSTS: any;
  // page: number = 1;
  // count: number = 0;
  // tableSize: number = 7;
  // tableSizes: any = [3, 6, 9, 12];
  // constructor(private postService: PostService,public service: PageAlertService) {}
  // ngOnInit(): void {
  //   this.fetchPosts();
  // }
  // fetchPosts(): void {
  //   this.postService.getAllPosts().subscribe(
  //     (response) => {
  //       this.POSTS = response;
  //       console.log(response);
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );
  // }
  // onTableDataChange(event: any) {
  //   this.page = event;
  //   this.fetchPosts();
  // }
  // onTableSizeChange(event: any): void {
  //   this.tableSize = event.target.value;
  //   this.page = 1;
  //   this.fetchPosts();
  // }


  // showWarning() {
  //   this.service.show({
  //     type: 'success',
  //     message:
  //       'We will have a service interruption between 02:00 AM and 04:00 AM at October 23, 2023!',
  //     title: 'Service Interruption',
  //   });
  // }
  items: MenuItem[];
  vac: any
  constructor(public messageService: MessageService,
    private compressImage: CompressImageService, public ticketService: TicketService, private elRef: ElementRef) { }
  subscription: Subscription;
  ngOnInit(): void {
    //this.items = [{
    //       label: 'Personal',
    //       routerLink: 'personal'
    //   },
    //   {
    //       label: 'Seat',
    //       routerLink: 'seat'
    //   },
    //   {
    //       label: 'Payment',
    //       routerLink: 'payment'
    //   },
    //   {
    //       label: 'Confirmation',
    //       routerLink: 'confirmation'
    //   }
    // ];
    // this.subscription = this.ticketService.paymentComplete$.subscribe((personalInformation) =>{
    //   this.messageService.add({severity:'success', summary:'Order submitted', detail: 'Dear, ' + personalInformation.firstname + ' ' + personalInformation.lastname + ' your order completed.'});
    // });
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }
  uploadss(event) {
    let image: File = event.target.files[0];
    this.compressImage
      .compress(image)
      .pipe(take(1))
      .subscribe((compressedImage) => {
        this.uploads(compressedImage);
      });
  }
  uploads(s) {
    //  const file = s?.target?.files[0];
    const reader = new FileReader();
    let byteArray;
    reader.onloadend = (e) => {
      this.vac = reader.result;
      if (this.vac != null) {
        var abcs = this.vac.indexOf(',');
        console.log(this.vac);
        // this.getuserforedit.extraProperties.Profilepic = this.vac.substring(
        //   abcs + 1
        // );
      }
    };
    if (s) {
      reader.readAsDataURL(s);
    }
  }

  upload(s) {
    const file = s?.target?.files[0];

    // const preview = document.getElementById('preview');
    var div = this.elRef.nativeElement.querySelector('#viwesss')
    const reader = new FileReader();
    let byteArray;

    reader.onloadend = (e) => {
      this.vac = reader.result;
      if (this.vac != null) {
        var abcs = this.vac.indexOf(',')
        // this.getuserforedit.extraProperties.Profilepic = this.vac.substring(abcs + 1)
        div.src = this.vac

      }

    };


    if (file) {
      reader.readAsDataURL(file);
    }
  }
}

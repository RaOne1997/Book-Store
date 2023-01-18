import {
  AuthService,
  ConfigStateService,
  CurrentUserDto,
  NAVIGATE_TO_MANAGE_PROFILE,
  SessionStateService,
} from '@abp/ng.core';
import { IdentityUserDto, IdentityUserService } from '@abp/ng.identity/proxy';
import { UserMenu, UserMenuService } from '@abp/ng.theme.shared';
import { Component, Inject, OnInit, TrackByFunction } from '@angular/core';
import { FileService } from '@proxy/blo-bstorage';
import { BlobDto, GetBlobRequestDto } from '@proxy/blob-storage';
import { Account } from '@proxy/volo/abp';
import { Observable } from 'rxjs';

@Component({
  selector: 'abp-current-user',
  templateUrl: './current-user.component.html',
})
export class CurrentUserComponent implements OnInit {
  currentUser$: Observable<CurrentUserDto> = this.configState.getOne$('currentUser');
  selectedTenant$ = this.sessionState.getTenant$();
  userdata={}as BlobDto

  trackByFn: TrackByFunction<UserMenu> = (_, element) => element.id;

  get smallScreen(): boolean {
    return window.innerWidth < 992;
  }
  asd={}as GetBlobRequestDto

  constructor(
    @Inject(NAVIGATE_TO_MANAGE_PROFILE) public readonly navigateToManageProfile: () => void,
    public readonly userMenu: UserMenuService,
    private authService: AuthService,
    private configState: ConfigStateService,
    private IdentityUser: IdentityUserService,
    private blobstorage: FileService,
    private sessionState: SessionStateService,
  
  ) {
   
  }
  ngOnInit(): void {
    this.displaydata() 

  }

    displaydata() {
    
    this.currentUser$.subscribe(rec=>{rec.id != null ?   (this.asd.name = rec.email,

      this.blobstorage.getBlob(this.asd).forEach(x=>{this.userdata.content = x.content}))
:null })
      
  }
   navigateToLogin() {
    this.authService.navigateToLogin();
  }

  logout() {
    this.authService.logout().subscribe();
  }
}

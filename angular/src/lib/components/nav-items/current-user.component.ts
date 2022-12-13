import {
  AuthService,
  ConfigStateService,
  CurrentUserDto,
  NAVIGATE_TO_MANAGE_PROFILE,
  SessionStateService,
} from '@abp/ng.core';
import { IdentityUserDto, IdentityUserService } from '@abp/ng.identity/proxy';
import { UserMenu, UserMenuService } from '@abp/ng.theme.shared';
import { Component, Inject, TrackByFunction } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'abp-current-user',
  templateUrl: './current-user.component.html',
})
export class CurrentUserComponent {
  currentUser$: Observable<CurrentUserDto> = this.configState.getOne$('currentUser');
  selectedTenant$ = this.sessionState.getTenant$();
  userdata: IdentityUserDto

  trackByFn: TrackByFunction<UserMenu> = (_, element) => element.id;

  get smallScreen(): boolean {
    return window.innerWidth < 992;
  }

  constructor(
    @Inject(NAVIGATE_TO_MANAGE_PROFILE) public readonly navigateToManageProfile: () => void,
    public readonly userMenu: UserMenuService,
    private authService: AuthService,
    private configState: ConfigStateService,
    private IdentityUser: IdentityUserService,

    private sessionState: SessionStateService,
  ) {


    this.currentUser$.subscribe(rec=>{rec.id != null ? IdentityUser.get(rec.id).forEach(x=>{ this.userdata=x ;   console.log(this.userdata)}):null})
    
 

  }

  navigateToLogin() {
    this.authService.navigateToLogin();
  }

  logout() {
    this.authService.logout().subscribe();
  }
}

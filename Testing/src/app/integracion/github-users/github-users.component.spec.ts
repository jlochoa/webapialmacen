import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GithubUsersComponent } from './github-users.component';
import { MockUsers } from '../users.class';
import { GithubUsersService } from '../github-users.service';

describe('GithubUsersComponent', () => {
  let component: GithubUsersComponent ;
  let fixture: ComponentFixture<GithubUsersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GithubUsersComponent],
      providers: [
        {
          provide: GithubUsersService,
          useClass: MockUsers
        }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GithubUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('debe crearse el componente', () => {
    expect(component).toBeTruthy();
  });

  it('debe cargarse un usuario', () => {
    component.getUsers();
    fixture.detectChanges();
    expect(component.users[0].login).toEqual('mojombo');
  });
});

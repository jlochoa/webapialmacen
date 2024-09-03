import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { GithubUsersService } from './github-users.service';

describe('GithubUsersService ', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [GithubUsersService]
    });
  });

  it('servicio creado', () => {
    const service: GithubUsersService = TestBed.inject(GithubUsersService);
    expect(service).toBeTruthy();
  });

  it('petición get exitosa', () => {
    const service: GithubUsersService = TestBed.inject(GithubUsersService );
    service.getUsers().subscribe({
      next: (response) => {
        expect(response).not.toBeNull();
      },
      error: (error) => fail(error)
    });
  });
});

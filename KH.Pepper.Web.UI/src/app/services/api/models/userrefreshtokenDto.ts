
export class UserRefreshTokenDto { 
    Id : number = 0;
    Token : string = '';
    RefreshToken : string = '';
    CreatedDate:Date = new Date();
    ExpirationDate:Date = new Date();
    IpAddress:string = '';
    IsInvalidated:boolean = false;
    UserId:  number = 0;
    
}
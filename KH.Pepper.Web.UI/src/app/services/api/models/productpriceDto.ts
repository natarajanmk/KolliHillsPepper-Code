
export class ProductPriceDto { 
    Id : number = 0;
    ProductId : number = 0;
    ProductQtyId : number = 0;
    Price:number = 0.00;
    IsOfferAvailable:boolean = false;
    OfferAmount:number = 0.00;
    OfferDetails:string = '';
    IsDisplayOnProduct:boolean = false;
   
}
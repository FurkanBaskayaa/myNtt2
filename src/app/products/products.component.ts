import { Component} from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { Router, ActivatedRoute, NavigationEnd} from '@angular/router';
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent{
  id: any;
  lng: any;

  smry:any;
  
  ProductIdsList:any=[];
  TranslatedProductsList:any=[];

  constructor(private service:SharedService, private route: ActivatedRoute, private router: Router) {
    this.router.events.subscribe((ev)=>{
      if(ev instanceof NavigationEnd)  {
        this.route.queryParams.subscribe(data => {
          if(data.id != null){
            this.id = data.id;
          }
          this.lng = data.lng;
        });
        this.ProductIdsList=[];
        this.TranslatedProductsList=[];

        const x = this.service.getProductsOfACatagory(this.id).subscribe((data:any)=>{
          for(let i = 0; i < data.length; i++){
            this.ProductIdsList.push(data[i].product_id);
          }
          this.service.getTranslatedProductList(this.ProductIdsList,this.lng).subscribe((data2:any)=>{
            this.TranslatedProductsList = data2;
          });
        });
      }
    });
  }
  onSummaryClick(val:any){
    this.smry = val;
  }
  
}

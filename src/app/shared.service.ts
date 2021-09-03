import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { NavigationExtras } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl="https://localhost:5001/api";
  constructor(private http:HttpClient) { }

  getCatagoryList(val:any):Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Catagory/'+val);
  }
  getProductsOfACatagory(val:any):Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/BelongsTo/'+val);
  }
  getTranslatedProductList(idArr:any[],lng:any):Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Products/'+lng,{
      params:{
        idArr:idArr
      }
    });
  }
 /* getProductList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Products');
  }

  addProduct(val:any):Observable<any[]>{
    return this.http.post<any>(this.APIUrl+'/Products', val);
  }

  updateProduct(val:any){
    return this.http.put(this.APIUrl+'/Products', val);
  }

  deleteProduct(val:any){
    return this.http.delete(this.APIUrl+'/Products/')
  }
*/
}

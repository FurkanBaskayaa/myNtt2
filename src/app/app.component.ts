import { Component, OnInit, } from '@angular/core';
import { SharedService } from './shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private service:SharedService, private router: Router) {}
  
  CatagoryList:any=[];
  language: any;
  id:any;

  ngOnInit(): void{
    this.language = 'english';
    this.getCatagoryList();
  }

  onCatagoryClick(val:any){
    this.id = val;
    this.router.navigate(['/products'], {queryParams: {id:this.id, lng:this.language}});
  }
  onLanguageClick(val:any){
    this.language=val;
    this.getCatagoryList();
    this.router.navigate(['/products'], {queryParams: {id:this.id, lng:this.language}});
  }
  getCatagoryList(){
    this.service.getCatagoryList(this.language).subscribe(data=>{
      this.CatagoryList = data;
    });
  }
}

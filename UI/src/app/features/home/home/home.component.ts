import { Component, OnInit } from '@angular/core';
import { HomeService } from '../services/home.service';
import { searchReturn } from '../models/home.models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  searchObj: string | null = null;

  searchReturnObj: searchReturn[] = [];

  constructor(private homeService: HomeService){} 

  ngOnInit(): void {
    if(this.searchObj != null){
      this.homeService.searchResult(this.searchObj)
      .subscribe({
        next: (searchreturn) =>{
          this.searchReturnObj = searchreturn;
        },
        error: (er) =>{
          alert(er?.error.message);
          
        }
      })
    }
  }


  

}

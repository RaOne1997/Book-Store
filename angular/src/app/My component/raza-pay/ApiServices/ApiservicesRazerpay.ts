import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
declare let Razorpay: any;
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { ScriptService } from "../ScriptService";

const httpOptions = {
    headers: new HttpHeaders({
     
      'Authorization': 'Basic ' + btoa('rzp_test_nhrFMzGsypzvM5:wPORnpniDaEMurRzKiT0YTm5')
    })
  };

@Injectable({
    providedIn: 'root'
})
export class RazerpayservicesApi {

  
    baseApiUrl: any = environment.url;
    constructor(private http: HttpClient,private RZPS: ScriptService) { 
        RZPS.load('Razorpay')}
    Getallpayment():Observable<any[]>{
        var rzp1 = new Razorpay("rzp_test_nhrFMzGsypzvM5","wPORnpniDaEMurRzKiT0YTm5")
       var  options ={
            "count": 2,
            "skip": 1,
          }
     return   rzp1.settlements.all(options)
        
      }
    

}
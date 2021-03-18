import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Solution } from 'src/models/solution-model';
import { ApiResponse } from 'src/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class SolutionsService {

  private baseUrl: string = "https://localhost:5001/api/packagesexplorer";

  constructor(private http: HttpClient) { }

  public GetSolutions(packageName: string) : Observable<Solution[]>
  {
    return this.http.get<ApiResponse<Solution>>(`${this.baseUrl}/${packageName}`)
      .pipe(
        map(response => response.resource)
      );
  }
}

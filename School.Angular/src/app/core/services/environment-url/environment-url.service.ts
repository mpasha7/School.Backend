import { Injectable } from '@angular/core';
import { environment } from '../../../../environmens/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentUrlService {
  public urlAddress: string = environment.urlAddress;

  constructor() { }
}

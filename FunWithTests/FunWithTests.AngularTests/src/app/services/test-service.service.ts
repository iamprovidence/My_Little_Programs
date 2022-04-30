import { Injectable } from '@angular/core';

export interface Data {
  value: string;
}

export class SpaceService
{
  public getSpace():string{
    return " ";
  }
}

@Injectable({
  providedIn: 'root'
})
export class TestService {

  constructor(
    private spaceService: SpaceService,
    private data:Data) { }

  public getFormattedData(postfix: string): string {
    return `${this.data.value}${this.spaceService.getSpace()}${postfix}`;
  }
}

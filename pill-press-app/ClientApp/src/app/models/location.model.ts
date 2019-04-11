import { CustomAddress } from './custom-address.model';

export interface Location {
  id: string,
  name: string,
  privateDwelling: string,
  //settingDescription: string, // this should be in Equipment Location
  address: CustomAddress
}

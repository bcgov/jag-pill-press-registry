import { CustomAddress } from './custom-address.model';
import { Equipment } from './equipment.model';
import { Location } from './location.model';

export interface EquipmentLocation {
  id: string;
  name: string;
  privateDwelling: string;
  settingDescription: string;
  fromWhen: Date;
  address: CustomAddress;
  equipment: Equipment,
  location: Location
}

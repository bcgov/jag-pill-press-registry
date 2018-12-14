import { CustomAddress } from './custom-address.model';
import { Application } from './application.model';

export interface EquipmentLocation {
    id: string;
    address: CustomAddress;
    application: Application;
    name: string;
    privateDwelling: boolean;
    settingDescription: string;
}

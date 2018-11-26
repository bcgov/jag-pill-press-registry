import { DynamicsAccount } from './dynamics-account.model';
import { AdoxioLicense } from './adoxio-license.model';
import { Invoice } from './invoice.model';

export class Application {
  id: string;
  borrowrentleaseequipment: boolean;
  currentlyownusepossessequipment: boolean;
  declarationofcorrectinformation: boolean;
  foippaconsent: boolean;
  intendtopurchaseequipment: boolean;
  mainbusinessfocus: string;
  manufacturingprocessdescription: string;
  ownintendtoownequipmentforbusinessuse: boolean;
  producingownproduct: boolean;
  providingmanufacturingtoothers: boolean;
  sellequipment: boolean;

  // related entities
  applicant: Account;
  customProducts: CustomProduct[];
}

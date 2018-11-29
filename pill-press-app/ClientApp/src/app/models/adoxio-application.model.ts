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
  foodanddrugact: boolean;
  legislativeauthorityother: string;
  kindsofproductsdrugs: boolean;
  kindsofproductsnaturalhealthproducts: boolean;
  kindsofproductsother: string;
  drugestablishmentlicence: boolean;
  sitelicence: boolean;
  otherlicence: string;
  delbusinessname: string;
  drugestablishmentlicencenumber: string;
  drugestablishmentlicenceexpirydate: Date;
  sitelicencebusinessname: string;
  sitelicencenumber: string;
  sitelicenceexpirydate: Date;
  otherlicencebusinessname: string;
  otherlicencenumber: string;
  otherlicenceexpirydate: Date;

  // related entities
  applicant: Account;
  customProducts: CustomProduct[];
}

import { DynamicsAccount } from './dynamics-account.model';
import { AdoxioLicense } from './adoxio-license.model';
import { Invoice } from './invoice.model';

export class Application {
  id: string;
  borrowrentleaseequipment: boolean;
  currentlyownusepossessequipment: boolean;
  declarationofcorrectinformation: boolean;
  delbusinessname: string;
  drugestablishmentlicence: boolean;
  drugestablishmentlicenceexpirydate: Date;
  drugestablishmentlicencenumber: string;
  foippaconsent: boolean;
  foodanddrugact: boolean;
  intendtopurchaseequipment: boolean;
  kindsofproductsdrugs: boolean;
  kindsofproductsnaturalhealthproducts: boolean;
  kindsofproductsother: string;
  kindsofproductsothercheck: string;
  legislativeauthorityother: string;
  legislativeauthorityothercheck: string;
  mainbusinessfocus: string;
  manufacturingprocessdescription: string;
  otherlicence: string;
  otherlicencebusinessname: string;
  otherlicencecheck: string;
  otherlicenceexpirydate: Date;
  otherlicencenumber: string;
  ownintendtoownequipmentforbusinessuse: boolean;
  producingownproduct: boolean;
  providingmanufacturingtoothers: boolean;
  sellequipment: boolean;
  sitelicence: boolean;
  sitelicencebusinessname: string;
  sitelicenceexpirydate: Date;
  sitelicencenumber: string;

  // related entities
  applicant: Account;
  customProducts: CustomProduct[];
}

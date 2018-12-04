import { DynamicsAccount } from './dynamics-account.model';
import { AdoxioLicense } from './adoxio-license.model';
import { Invoice } from './invoice.model';
import { DynamicsContact } from './dynamics-contact.model';

export class Application {
  id: string;
  additionalbusinessinformationaboutseller: string;
  borrowrentleaseequipment: boolean;
  currentlyownusepossessequipment: boolean;
  declarationofcorrectinformation: boolean;
  delbusinessname: string;
  drugestablishmentlicence: boolean;
  drugestablishmentlicenceexpirydate: Date;
  drugestablishmentlicencenumber: string;
  foippaconsent: boolean;
  foodanddrugact: boolean;
  intendonrentingleasingtoothers: boolean;
  intendonsellingequipmenttoothers: boolean;
  intendtopurchaseequipment: boolean;
  intendtoselldiemouldorpunch: boolean;
  intendtosellencapsulator: boolean;
  intendtosellother: string;
  intendtosellothercheck: boolean;
  intendtosellpharmaceuticalmixerorblender: boolean;
  intendtosellpillpress: boolean;
  kindsofproductsdrugs: boolean;
  kindsofproductsnaturalhealthproducts: boolean;
  kindsofproductsother: string;
  kindsofproductsothercheck: string;
  legislativeauthorityother: string;
  legislativeauthorityothercheck: string;
  mainbusinessfocus: string;
  manufacturerofcontrolledequipment: boolean;
  manufacturingprocessdescription: string;
  onetimesellerofowncontrolledequipment: boolean;
  otherlicence: string;
  otherlicencebusinessname: string;
  otherlicencecheck: string;
  otherlicenceexpirydate: Date;
  otherlicencenumber: string;
  ownintendtoownequipmentforbusinessuse: boolean;
  ownusepossesstoproduceaproduct: boolean;
  producingownproduct: boolean;
  providingmanufacturingtoothers: boolean;
  retailerofcontrolledequipment: boolean;
  sellequipment: boolean;
  sitelicence: boolean;
  sitelicencebusinessname: string;
  sitelicenceexpirydate: Date;
  sitelicencenumber: string;
  typeofsellerother: string;
  typeofsellerothercheck: boolean;

  // related entities
  applicant: Account;
  customProducts: CustomProduct[];
  ownersAndManagers: DynamicsContact[];
}
